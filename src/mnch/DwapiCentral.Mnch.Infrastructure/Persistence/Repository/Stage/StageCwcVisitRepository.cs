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
    public class StageCwcVisitRepository : IStageCwcVisitRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MnchDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageCwcVisitRepository(MnchDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageCwcVisit)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageCwcVisit> extracts, Guid manifestId)
        {
            try
            {
                var pks = extracts.Select(x => x.Id).ToList();

                var result = await StageData(manifestId, pks);

                if (result == 0)
                {
                    // stage > Rest
                    _context.Database.GetDbConnection().BulkInsert(extracts);
                }

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "CwcVisitExtract" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageCwcVisit> stageCwcVisit)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageCwcVisit> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*
                            FROM CwcVisits p 
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

                var existingRecords = await connection.QueryAsync<CwcVisit>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string? RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageCwcVisit
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageCwcVisit, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageCwcVisit;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageCwcVisit> uniqueStageExtracts)
        {
            try
            {
                
                var latestRecordsDict = new Dictionary<string, StageCwcVisit>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<CwcVisit>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageCwcVisit> stageDrug, IEnumerable<CwcVisit> existingRecords)
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
                //                     CwcVisits

                //               SET                                  
                //                    DateExtracted = @DateExtracted,                                   
                //                    FacilityName = @FacilityName,                                    
                //                    VisitDate = @VisitDate,
                //                    VisitID = @VisitID,
                //                    Height = @Height,
                //                    Weight = @Weight,
                //                    Temp = @Temp,
                //                    PulseRate = @PulseRate,
                //                    RespiratoryRate = @RespiratoryRate,
                //                    OxygenSaturation = @OxygenSaturation,
                //                    MUAC = @MUAC,
                //                    WeightCategory = @WeightCategory,
                //                    Stunted = @Stunted,
                //                    InfantFeeding = @InfantFeeding,
                //                    MedicationGiven = @MedicationGiven,
                //                    TBAssessment = @TBAssessment,
                //                    MNPsSupplementation = @MNPsSupplementation,
                //                    Immunization = @Immunization,
                //                    DangerSigns = @DangerSigns,
                //                    Milestones = @Milestones,
                //                    VitaminA = @VitaminA,
                //                    Disability = @Disability,
                //                    ReceivedMosquitoNet = @ReceivedMosquitoNet,
                //                    Dewormed = @Dewormed,
                //                    ReferredFrom = @ReferredFrom,
                //                    ReferredTo = @ReferredTo,
                //                    ReferralReasons = @ReferralReasons,
                //                    FollowUP = @FollowUP,
                //                    NextAppointment = @NextAppointment,
                //                    Date_Created = @Date_Created,
                //                    Date_Last_Modified = @Date_Last_Modified,
                //                    HeightLength = @HeightLength,
                //                    Refferred = @Refferred,
                //                    RevisitThisYear = @RevisitThisYear,
                //                    ZScore = @ZScore,
                //                    ZScoreAbsolute = @ZScoreAbsolute,
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

        private async Task<int> StageData(Guid manifestId, List<Guid> ids)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    ids

                };

                var query = $@"
                           
                                    SELECT 1
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND Id IN @ids                                   
                             
                        ";

                var result = await connection.QueryFirstOrDefaultAsync<int>(query, queryParameters);

                return result;

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
    }
}
