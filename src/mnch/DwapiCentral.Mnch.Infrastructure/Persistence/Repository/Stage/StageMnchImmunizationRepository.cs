using AutoMapper;
using DwapiCentral.Mnch.Domain.Model.Stage;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using log4net;
using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;
using DwapiCentral.Mnch.Domain.Events;
using Dapper;

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Repository.Stage
{
    public class StageMnchImmunizationRepository : IStageMnchImmunizationRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MnchDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageMnchImmunizationRepository(MnchDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageMnchImmunization)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageMnchImmunization> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var pks = extracts.Select(x => x.Id).ToList();

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);


                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "MnchImmunizationExtract" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageMnchImmunization> stageMnchImmunization)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageMnchImmunization> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*

                            FROM MnchImmunizations p

                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT PatientPK, SiteCode, RecordUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.RecordUUID = s.RecordUUID                                   
                                                                   
                            )
                        ";

                var existingRecords = await connection.QueryAsync<MnchImmunization>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageMnchImmunization
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageMnchImmunization, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageMnchImmunization;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageMnchImmunization> uniqueStageExtracts)
        {
            try
            {

                var latestRecordsDict = new Dictionary<string, StageMnchImmunization>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<MnchImmunization>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageMnchImmunization> stageDrug, IEnumerable<MnchImmunization> existingRecords)
        {
            try
            {
                //Update existing data
                var stageDictionary = stageDrug
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                         .ToDictionary(
                             g => g.Key,
                             g => g.FirstOrDefault()
                         );

                foreach (var existingExtract in existingRecords)
                {
                    if (stageDictionary.TryGetValue(
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.RecordUUID },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }
                _context.Database.GetDbConnection().BulkMerge(existingRecords);
                //var cons = _context.Database.GetConnectionString();
                //var sql = $@"
                //           UPDATE 
                //                     MnchImmunizations

                //               SET                                  
                //                    DateExtracted = @DateExtracted,                                   
                //                    FacilityName = @FacilityName,                                   
                //                    BCG = @BCG,
                //                    OPVatBirth = @OPVatBirth,
                //                    OPV1 = @OPV1,
                //                    OPV2 = @OPV2,
                //                    OPV3 = @OPV3,
                //                    IPV = @IPV,
                //                    DPTHepBHIB1 = @DPTHepBHIB1,
                //                    DPTHepBHIB2 = @DPTHepBHIB2,
                //                    DPTHepBHIB3 = @DPTHepBHIB3,
                //                    PCV101 = @PCV101,
                //                    PCV102 = @PCV102,
                //                    PCV103 = @PCV103,
                //                    ROTA1 = @ROTA1,
                //                    MeaslesReubella1 = @MeaslesReubella1,
                //                    YellowFever = @YellowFever,
                //                    MeaslesReubella2 = @MeaslesReubella2,
                //                    MeaslesAt6Months = @MeaslesAt6Months,
                //                    ROTA2 = @ROTA2,
                //                    DateOfNextVisit = @DateOfNextVisit,
                //                    BCGScarChecked = @BCGScarChecked,
                //                    DateChecked = @DateChecked,
                //                    DateBCGrepeated = @DateBCGrepeated,
                //                    VitaminAAt6Months = @VitaminAAt6Months,
                //                    VitaminAAt1Yr = @VitaminAAt1Yr,
                //                    VitaminAAt18Months = @VitaminAAt18Months,
                //                    VitaminAAt2Years = @VitaminAAt2Years,
                //                    VitaminAAt2To5Years = @VitaminAAt2To5Years,
                //                    FullyImmunizedChild = @FullyImmunizedChild,
                //                    Date_Created = @Date_Created,
                //                    DateLastModified = @DateLastModified,
                //                    Created = @Created,
                //                    Updated = @Updated,
                //                    Voided = @Voided   

                //             WHERE  PatientPk = @PatientPK
                //                    AND SiteCode = @SiteCode
                //                    AND RecordUUID = @RecordUUID";

                //using var connection = new SqlConnection(cons);
                //if (connection.State != ConnectionState.Open)
                //    connection.Open();
                //await connection.ExecuteAsync(sql, existingRecords);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateLivestage(Guid manifestId, List<Guid> ids)
        {

            var cons = _context.Database.GetConnectionString();

            var sql = $@"
                            UPDATE 
                                    {_stageName}
                            SET 
                                    LiveStage= @nextlivestage 
                            
                            WHERE 
                                    ManifestId = @manifestId AND 
                                    LiveStage= @livestage AND
                                    Id IN @ids";
            try
            {
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                await connection.ExecuteAsync($"{sql}",
                    new
                    {
                        manifestId,
                        livestage = LiveStage.Rest,
                        nextlivestage = LiveStage.Assigned,
                        ids
                    }, null, 0);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }
    }
}
