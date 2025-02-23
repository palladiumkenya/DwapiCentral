using System.Data;
using System.Reflection;
using AutoMapper;
using Dapper;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage;
using DwapiCentral.Shared.Domain.Enums;
using log4net;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageEnhancedAdherenceCounsellingExtractRepository :IStageEnhancedAdherenceCounsellingExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageEnhancedAdherenceCounsellingExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageEnhancedAdherenceCounsellingExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageEnhancedAdherenceCounsellingExtract> extracts, Guid manifestId)
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

                // assign > Assigned
                await AssignAll(manifestId, extracts.Select(x => x.Id).ToList());

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "EnhancedAdherenceCounsellingExtract" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageEnhancedAdherenceCounsellingExtract> stageEnhancedAdherance)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageEnhancedAdherenceCounsellingExtract> uniqueStageExtracts;
                await connection.OpenAsync();
                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Assigned
                };
                var query = $@"
                            SELECT p.*
                            FROM EnhancedAdherenceCounsellingExtract p 
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT DISTINCT PatientPK, SiteCode, RecordUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        LiveSession = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.RecordUUID = s.RecordUUID                                  
                                                                      
                            )
                        ";

               
                var existingRecords = await connection.QueryAsync<EnhancedAdherenceCounsellingExtract>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageEnhancedAdherance
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID)) && x.LiveSession == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageEnhancedAdherance, existingRecords,manifestId);
                }
                else
                {
                    uniqueStageExtracts = stageEnhancedAdherance;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts,manifestId);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageEnhancedAdherenceCounsellingExtract> uniqueStageExtracts,Guid manifestId)
        {

            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageEnhancedAdherenceCounsellingExtract>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<EnhancedAdherenceCounsellingExtract>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                var notification = new OnErrorEvent { ExtractName = "EnhancedAdherenceCounsellingExtract", ManifestId = manifestId, SiteCode = uniqueStageExtracts.First().SiteCode, message = ex.Message };
                await _mediator.Publish(notification);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageEnhancedAdherenceCounsellingExtract> stageEnhancedAdherance, IEnumerable<EnhancedAdherenceCounsellingExtract> existingRecords,Guid manifestId)
        {
           

                try
                {
                    var centraldata = stageEnhancedAdherance.Select(_mapper.Map<StageEnhancedAdherenceCounsellingExtract, EnhancedAdherenceCounsellingExtract>).ToList();

                    centraldata = centraldata.GroupBy(x => x.RecordUUID).Select(g => g.First()).ToList();

                    var existingIds = existingRecords.Select(x => x.RecordUUID).ToHashSet();

                    var recordsToUpdate = centraldata.Join(existingIds, x => x.RecordUUID, y => y, (x, y) => x).ToList();


                    const int maxRetries = 3;

                    for (var retry = 0; retry < maxRetries; retry++)
                    {
                        try
                        {
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 1205)
                            {
                                _context.Database.GetDbConnection().BulkUpdate(recordsToUpdate);
                                await Task.Delay(100);
                            }
                            else
                            {
                                Log.Error(ex);
                                var notification = new OnErrorEvent { ExtractName = "EnhancedAdherenceCounsellingExtract", ManifestId = manifestId, SiteCode = existingRecords.First().SiteCode, message = ex.Message };
                                await _mediator.Publish(notification);
                                throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    throw;
                }
                //    var cons = _context.Database.GetConnectionString();
                //    using (var connection = new SqlConnection(cons))
                //    {
                //        await connection.OpenAsync();

                //        using (var transaction = connection.BeginTransaction())
                //        {
                //            const int maxRetries = 3;

                //            for (var retry = 0; retry < maxRetries; retry++)
                //            {
                //                try
                //                {
                //                    var sql = $@"
                //               UPDATE 
                //                         EnhancedAdherenceCounsellingExtract

                //                   SET                                  
                //                        VisitID = @VisitID,
                //                        VisitDate = @VisitDate,
                //                        SessionNumber = @SessionNumber,
                //                        DateOfFirstSession = @DateOfFirstSession,
                //                        PillCountAdherence = @PillCountAdherence,
                //                        MMAS4_1 = @MMAS4_1,
                //                        MMAS4_2 = @MMAS4_2,
                //                        MMAS4_3 = @MMAS4_3,
                //                        MMAS4_4 = @MMAS4_4,
                //                        MMSA8_1 = @MMSA8_1,
                //                        MMSA8_2 = @MMSA8_2,
                //                        MMSA8_3 = @MMSA8_3,
                //                        MMSA8_4 = @MMSA8_4,
                //                        MMSAScore = @MMSAScore,
                //                        EACRecievedVL = @EACRecievedVL,
                //                        EACVL = @EACVL,
                //                        EACVLConcerns = @EACVLConcerns,
                //                        EACVLThoughts = @EACVLThoughts,
                //                        EACWayForward = @EACWayForward,
                //                        EACCognitiveBarrier = @EACCognitiveBarrier,
                //                        EACBehaviouralBarrier_1 = @EACBehaviouralBarrier_1,
                //                        EACBehaviouralBarrier_2 = @EACBehaviouralBarrier_2,
                //                        EACBehaviouralBarrier_3 = @EACBehaviouralBarrier_3,
                //                        EACBehaviouralBarrier_4 = @EACBehaviouralBarrier_4,
                //                        EACBehaviouralBarrier_5 = @EACBehaviouralBarrier_5,
                //                        EACEmotionalBarriers_1 = @EACEmotionalBarriers_1,
                //                        EACEmotionalBarriers_2 = @EACEmotionalBarriers_2,
                //                        EACEconBarrier_1 = @EACEconBarrier_1,
                //                        EACEconBarrier_2 = @EACEconBarrier_2,
                //                        EACEconBarrier_3 = @EACEconBarrier_3,
                //                        EACEconBarrier_4 = @EACEconBarrier_4,
                //                        EACEconBarrier_5 = @EACEconBarrier_5,
                //                        EACEconBarrier_6 = @EACEconBarrier_6,
                //                        EACEconBarrier_7 = @EACEconBarrier_7,
                //                        EACEconBarrier_8 = @EACEconBarrier_8,
                //                        EACReviewImprovement = @EACReviewImprovement,
                //                        EACReviewMissedDoses = @EACReviewMissedDoses,
                //                        EACReviewStrategy = @EACReviewStrategy,
                //                        EACReferral = @EACReferral,
                //                        EACReferralApp = @EACReferralApp,
                //                        EACReferralExperience = @EACReferralExperience,
                //                        EACHomevisit = @EACHomevisit,
                //                        EACAdherencePlan = @EACAdherencePlan,
                //                        EACFollowupDate = @EACFollowupDate,
                //                        Date_Created = @Date_Created,
                //                        DateLastModified = @DateLastModified,
                //                        DateExtracted = @DateExtracted,
                //                        Created = @Created,
                //                        Updated = @Updated,
                //                        Voided = @Voided                          

                //                 WHERE   RecordUUID = @RecordUUID";

                //        await connection.ExecuteAsync(sql, recordsToUpdate,transaction);
                //                    transaction.Commit();
                //                    break;
                //                }
                //                catch (SqlException ex)
                //                {
                //                    if (ex.Number == 1205)
                //                    {

                //                        await Task.Delay(100);
                //                    }
                //                    else
                //                    {
                //                        transaction.Rollback();
                //                        throw;
                //                    }
                //                }
                //            }
                //        }
                //    }
         
            //try
            //{
            //    //Update existing data
            //    var stageDictionary = stageEnhancedAdherance
            //             .GroupBy(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
            //             .ToDictionary(
            //                 g => g.Key,
            //                 g => g.OrderByDescending(x => x.Date_Created).FirstOrDefault()
            //             );

            //    var updateTasks = existingRecords.Select(async existingExtract =>
            //    {
            //        if (stageDictionary.TryGetValue(
            //            new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.RecordUUID },
            //            out var stageExtract)
            //        )
            //        {
            //            _mapper.Map(stageExtract, existingExtract);
            //        }
            //    }).ToList();

            //    await Task.WhenAll(updateTasks);

            //     _context.Database.GetDbConnection().BulkUpdate(existingRecords);

            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex);
            //    throw;
            //}
        }

        private async Task AssignAll(Guid manifestId, List<Guid> ids)
        {
            var cons = _context.Database.GetConnectionString();

            var sql = $@"
                    UPDATE 
                            {_stageName}
                    SET 
                            LiveStage = @nextlivestage 
                    WHERE 
                            LiveSession = @manifestId AND 
                            LiveStage = @livestage AND 
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

        private async Task UpdateLivestage(Guid manifestId, List<Guid> ids)
        {

            var cons = _context.Database.GetConnectionString();

            var sql = $@"
                            UPDATE 
                                    {_stageName}
                            SET 
                                    LiveStage= @nextlivestage 
                            
                            WHERE 
                                    LiveSession = @manifestId AND 
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
                        livestage = LiveStage.Assigned,
                        nextlivestage = LiveStage.Merged,
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
                                        LiveSession = @manifestId 
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
