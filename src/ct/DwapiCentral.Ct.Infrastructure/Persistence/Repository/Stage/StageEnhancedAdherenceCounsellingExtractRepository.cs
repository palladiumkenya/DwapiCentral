using System.Data;
using System.Reflection;
using AutoMapper;
using Dapper;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models.Extracts;
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
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "EnhancedAdherenceCounsellingExtract" };
                await _mediator.Publish(notification);

                var pks = extracts.Select(x => x.Id).ToList();

                // assign > Assigned
                await AssignAll(manifestId, extracts.Select(x => x.Id).ToList());

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);

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
                            FROM EnhancedAdherenceCounsellingExtracts p 
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT PatientPK, SiteCode, RecordUUID, MAX(Date_Created) AS MaxCreatedTime
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        LiveSession = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.RecordUUID = s.RecordUUID                                  
                                    AND p.Date_Created = s.MaxCreatedTime                                    
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

                    await UpdateCentralDataWithStagingData(stageEnhancedAdherance, existingRecords);
                }
                else
                {
                    uniqueStageExtracts = stageEnhancedAdherance;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageEnhancedAdherenceCounsellingExtract> uniqueStageExtracts)
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
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageEnhancedAdherenceCounsellingExtract> stageEnhancedAdherance, IEnumerable<EnhancedAdherenceCounsellingExtract> existingRecords)
        {
            try
            {
                //Update existing data
                var stageDictionary = stageEnhancedAdherance
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                         .ToDictionary(
                             g => g.Key,
                             g => g.OrderByDescending(x => x.Date_Created).FirstOrDefault()
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

                var cons = _context.Database.GetConnectionString();
                var sql = $@"
                           UPDATE 
                                     EnhancedAdherenceCounsellingExtracts

                               SET                                  
                                    VisitID = @VisitID,
                                    VisitDate = @VisitDate,
                                    SessionNumber = @SessionNumber,
                                    DateOfFirstSession = @DateOfFirstSession,
                                    PillCountAdherence = @PillCountAdherence,
                                    MMAS4_1 = @MMAS4_1,
                                    MMAS4_2 = @MMAS4_2,
                                    MMAS4_3 = @MMAS4_3,
                                    MMAS4_4 = @MMAS4_4,
                                    MMSA8_1 = @MMSA8_1,
                                    MMSA8_2 = @MMSA8_2,
                                    MMSA8_3 = @MMSA8_3,
                                    MMSA8_4 = @MMSA8_4,
                                    MMSAScore = @MMSAScore,
                                    EACRecievedVL = @EACRecievedVL,
                                    EACVL = @EACVL,
                                    EACVLConcerns = @EACVLConcerns,
                                    EACVLThoughts = @EACVLThoughts,
                                    EACWayForward = @EACWayForward,
                                    EACCognitiveBarrier = @EACCognitiveBarrier,
                                    EACBehaviouralBarrier_1 = @EACBehaviouralBarrier_1,
                                    EACBehaviouralBarrier_2 = @EACBehaviouralBarrier_2,
                                    EACBehaviouralBarrier_3 = @EACBehaviouralBarrier_3,
                                    EACBehaviouralBarrier_4 = @EACBehaviouralBarrier_4,
                                    EACBehaviouralBarrier_5 = @EACBehaviouralBarrier_5,
                                    EACEmotionalBarriers_1 = @EACEmotionalBarriers_1,
                                    EACEmotionalBarriers_2 = @EACEmotionalBarriers_2,
                                    EACEconBarrier_1 = @EACEconBarrier_1,
                                    EACEconBarrier_2 = @EACEconBarrier_2,
                                    EACEconBarrier_3 = @EACEconBarrier_3,
                                    EACEconBarrier_4 = @EACEconBarrier_4,
                                    EACEconBarrier_5 = @EACEconBarrier_5,
                                    EACEconBarrier_6 = @EACEconBarrier_6,
                                    EACEconBarrier_7 = @EACEconBarrier_7,
                                    EACEconBarrier_8 = @EACEconBarrier_8,
                                    EACReviewImprovement = @EACReviewImprovement,
                                    EACReviewMissedDoses = @EACReviewMissedDoses,
                                    EACReviewStrategy = @EACReviewStrategy,
                                    EACReferral = @EACReferral,
                                    EACReferralApp = @EACReferralApp,
                                    EACReferralExperience = @EACReferralExperience,
                                    EACHomevisit = @EACHomevisit,
                                    EACAdherencePlan = @EACAdherencePlan,
                                    EACFollowupDate = @EACFollowupDate,
                                    Date_Created = @Date_Created,
                                    DateLastModified = @DateLastModified,
                                    DateExtracted = @DateExtracted,
                                    Created = @Created,
                                    Updated = @Updated,
                                    Voided = @Voided                          

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
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
    }
}
