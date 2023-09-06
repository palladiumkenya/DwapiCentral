using AutoMapper;
using DwapiCentral.Hts.Domain.Model.Stage;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Infrastructure.Persistence.Context;
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
using DwapiCentral.Hts.Domain.Repository.Stage;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;
using DwapiCentral.Hts.Domain.Events;
using Dapper;

namespace DwapiCentral.Hts.Infrastructure.Persistence.Repository.Stage
{
    public class StageHtsClientLinkageRepository : IStageHtsClientLinkageRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HtsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageHtsClientLinkageRepository(HtsDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageHtsClientLinkage)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageHtsClientLinkage> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

               
                var pks = extracts.Select(x => x.Id).ToList();

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "HtsClientLinkage" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageHtsClientLinkage> stageClientLinkage)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageHtsClientLinkage> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*
                            FROM HtsClientLinkages p 
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT PatientPK, SiteCode, HtsNumber, RecordUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, HtsNumber, RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.HtsNumber = s.HtsNumber                                   
                                    AND p.RecordUUID = s.RecordUUID                                    
                            )
                        ";

                var existingRecords = await connection.QueryAsync<HtsClientLinkage>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string HtsNumber,  string? RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.HtsNumber,  x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageClientLinkage
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.HtsNumber,  x.RecordUUID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageClientLinkage, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageClientLinkage;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageHtsClientLinkage> uniqueStageExtracts)
        {
            try
            {                
                var latestRecordsDict = new Dictionary<string, StageHtsClientLinkage>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.HtsNumber}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<HtsClientLinkage>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageHtsClientLinkage> stageDrug, IEnumerable<HtsClientLinkage> existingRecords)
        {
            try
            {
                //Update existing data
                var stageDictionary = stageDrug
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.HtsNumber, x.RecordUUID })
                         .ToDictionary(
                             g => g.Key,
                             g => g.FirstOrDefault()
                         );

                foreach (var existingExtract in existingRecords)
                {
                    if (stageDictionary.TryGetValue(
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.HtsNumber, existingExtract.RecordUUID },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }

                var cons = _context.Database.GetConnectionString();
                var sql = $@"
                           UPDATE 
                                     HtsClientLinkages

                               SET                                  
                                    DateEnrolled = @DateEnrolled,
                                    FacilityName = @FacilityName,
                                    EnrolledFacilityName = @EnrolledFacilityName,
                                    ReferralDate = @ReferralDate,
                                    DatePrefferedToBeEnrolled = @DatePrefferedToBeEnrolled,
                                    FacilityReferredTo = @FacilityReferredTo,
                                    HandedOverTo = @HandedOverTo,
                                    HandedOverToCadre = @HandedOverToCadre,
                                    ReportedCCCNumber = @ReportedCCCNumber,
                                    ReportedStartARTDate = @ReportedStartARTDate,
                                    Date_Created = @Date_Created,
                                    DateLastModified = @DateLastModified,
                                    DateExtracted = @DateExtracted,
                                    Created = @Created,
                                    Updated = @Updated,
                                    Voided = @Voided                          

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND HtsNumber = @HtsNumber
                                    AND RecordUUID = @RecordUUID";

                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                await connection.ExecuteAsync(sql, existingRecords);
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
