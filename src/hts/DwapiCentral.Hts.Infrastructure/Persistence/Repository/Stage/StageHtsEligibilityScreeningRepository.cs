using AutoMapper;
using Dapper;
using DwapiCentral.Hts.Domain.Events;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Model.Stage;
using DwapiCentral.Hts.Domain.Repository.Stage;
using DwapiCentral.Hts.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using log4net;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DwapiCentral.Hts.Infrastructure.Persistence.Repository.Stage
{
    public class StageHtsEligibilityScreeningRepository : IStageHtsEligibilityScreeningRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HtsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageHtsEligibilityScreeningRepository(HtsDbContext context, IMapper mapper, IMediator mediator, string stageName = $"StageHtsEligibilityExtract")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageHtsEligibilityScreening> extracts, Guid manifestId)
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

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "HtsEligibilityExtract" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageHtsEligibilityScreening> stageEligibilityScreening)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageHtsEligibilityScreening> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*
                            FROM HtsEligibilityExtract p 
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT PatientPK, SiteCode, HtsNumber,RecordUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, HtsNumber,RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.HtsNumber = s.HtsNumber  
                                    AND p.RecordUUID = s.RecordUUID
                                   
                            )
                        ";

                var existingRecords = await connection.QueryAsync<HtsEligibilityScreening>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string HtsNumber, string? RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.HtsNumber, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageEligibilityScreening
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.HtsNumber,x.RecordUUID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageEligibilityScreening, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageEligibilityScreening;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageHtsEligibilityScreening> uniqueStageExtracts)
        {
            try
            {
                var latestRecordsDict = new Dictionary<string, StageHtsEligibilityScreening>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.HtsNumber}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<HtsEligibilityScreening>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageHtsEligibilityScreening> stageDrug, IEnumerable<HtsEligibilityScreening> existingRecords)
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
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.HtsNumber,existingExtract.RecordUUID },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }

                var cons = _context.Database.GetConnectionString();
                var sql = $@"
                           UPDATE 
                                     HtsEligibilityExtract

                               SET                                  
                                    EncounterId = @EncounterId,
                                    VisitID = @VisitID,
                                    VisitDate = @VisitDate,
                                    PopulationType = @PopulationType,
                                    KeyPopulation = @KeyPopulation,
                                    PriorityPopulation = @PriorityPopulation,
                                    Department = @Department,
                                    PatientType = @PatientType,
                                    IsHealthWorker = @IsHealthWorker,
                                    RelationshipWithContact = @RelationshipWithContact,
                                    TestedHIVBefore = @TestedHIVBefore,
                                    WhoPerformedTest = @WhoPerformedTest,
                                    ResultOfHIV = @ResultOfHIV,
                                    StartedOnART = @StartedOnART,
                                    CCCNumber = @CCCNumber,
                                    EverHadSex = @EverHadSex,
                                    SexuallyActive = @SexuallyActive,
                                    NewPartner = @NewPartner,
                                    PartnerHIVStatus = @PartnerHIVStatus,
                                    CoupleDiscordant = @CoupleDiscordant,
                                    MultiplePartners = @MultiplePartners,
                                    NumberOfPartners = @NumberOfPartners,
                                    AlcoholSex = @AlcoholSex,
                                    MoneySex = @MoneySex,
                                    CondomBurst = @CondomBurst,
                                    UnknownStatusPartner = @UnknownStatusPartner,
                                    KnownStatusPartner = @KnownStatusPartner,
                                    Pregnant = @Pregnant,
                                    BreastfeedingMother = @BreastfeedingMother,
                                    ExperiencedGBV = @ExperiencedGBV,
                                    EverOnPrep = @EverOnPrep,
                                    CurrentlyOnPrep = @CurrentlyOnPrep,
                                    EverOnPep = @EverOnPep,
                                    CurrentlyOnPep = @CurrentlyOnPep,
                                    EverHadSTI = @EverHadSTI,
                                    CurrentlyHasSTI = @CurrentlyHasSTI,
                                    EverHadTB = @EverHadTB,
                                    SharedNeedle = @SharedNeedle,
                                    NeedleStickInjuries = @NeedleStickInjuries,
                                    TraditionalProcedures = @TraditionalProcedures,
                                    ChildReasonsForIneligibility = @ChildReasonsForIneligibility,
                                    EligibleForTest = @EligibleForTest,
                                    ReasonsForIneligibility = @ReasonsForIneligibility,
                                    SpecificReasonForIneligibility = @SpecificReasonForIneligibility,
                                    MothersStatus = @MothersStatus,
                                    DateTestedSelf = @DateTestedSelf,
                                    ResultOfHIVSelf = @ResultOfHIVSelf,
                                    DateTestedProvider = @DateTestedProvider,
                                    ScreenedTB = @ScreenedTB,
                                    Cough = @Cough,
                                    Fever = @Fever,
                                    WeightLoss = @WeightLoss,
                                    NightSweats = @NightSweats,
                                    Lethargy = @Lethargy,
                                    TBStatus = @TBStatus,
                                    ReferredForTesting = @ReferredForTesting,
                                    AssessmentOutcome = @AssessmentOutcome,
                                    TypeGBV = @TypeGBV,
                                    ForcedSex = @ForcedSex,
                                    ReceivedServices = @ReceivedServices,
                                    ContactWithTBCase = @ContactWithTBCase,
                                    Disability = @Disability,
                                    DisabilityType = @DisabilityType,
                                    HTSStrategy = @HTSStrategy,
                                    HTSEntryPoint = @HTSEntryPoint,
                                    HIVRiskCategory = @HIVRiskCategory,
                                    ReasonRefferredForTesting = @ReasonRefferredForTesting,
                                    ReasonNotReffered = @ReasonNotReffered,
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
