using System.Data;
using System.Reflection;
using AutoMapper;
using Dapper;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using log4net;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageVisitExtractRepository :IStageVisitExtractRepository
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageVisitExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageVisitExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageVisitExtract> extracts, Guid manifestId)
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
                await AssignAll(manifestId, pks);

                // Merge
                await MergeExtracts(manifestId, extracts);


                await UpdateLivestage(manifestId, pks);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PatientVisitExtract" };
                await _mediator.Publish(notification);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageVisitExtract> stageVisits)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageVisitExtract> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Assigned                   
                };
                var query = $@"
                            SELECT p.*
                            FROM PatientVisitExtract p
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
                
                var existingRecords = await connection.QueryAsync<PatientVisitExtract>(query, queryParameters);
                
                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.RecordUUID)));

                if (existingRecordsSet.Any())               {                  
                                                   
                    uniqueStageExtracts = stageVisits
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID)) && x.LiveSession == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageVisits,existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageVisits;
                }

                await InsertNewDataFromStaging(uniqueStageExtracts);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageVisitExtract> uniqueStageExtracts)
        {
            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageVisitExtract>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<PatientVisitExtract>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch(Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageVisitExtract> stageVisits, IEnumerable<PatientVisitExtract> existingRecords)
        {
            try
            {

                var centraldata = stageVisits.Select(_mapper.Map<StageVisitExtract, PatientVisitExtract>).ToList();


                var existingIptIds = existingRecords.Select(x => x.RecordUUID).ToHashSet();


                var recordsToUpdate = centraldata.Where(x => existingIptIds.Contains(x.RecordUUID)).ToList();

                var cons = _context.Database.GetConnectionString();
                using (var connection = new SqlConnection(cons))
                {
                    await connection.OpenAsync();

                    using (var transaction = connection.BeginTransaction())
                    {
                        const int maxRetries = 3; 

                        for (var retry = 0; retry < maxRetries; retry++)
                        {
                            try
                            {



                                var sql = $@"
                           UPDATE 
                                     PatientVisitExtract

                               SET
                                    VisitID = @VisitID,
                                    VisitDate = @VisitDate,                                    
                                    Service = @Service,
                                    VisitType = @VisitType,
                                    WHOStage = @WHOStage,
                                    WABStage = @WABStage,
                                    Pregnant = @Pregnant,
                                    LMP = @LMP,
                                    EDD = @EDD,
                                    Height = @Height,
                                    Weight = @Weight,
                                    BP = @BP,
                                    OI = @OI,
                                    OIDate = @OIDate,
                                    SubstitutionFirstlineRegimenDate = @SubstitutionFirstlineRegimenDate,
                                    SubstitutionFirstlineRegimenReason = @SubstitutionFirstlineRegimenReason,
                                    SubstitutionSecondlineRegimenDate = @SubstitutionSecondlineRegimenDate,
                                    SubstitutionSecondlineRegimenReason = @SubstitutionSecondlineRegimenReason,
                                    SecondlineRegimenChangeDate = @SecondlineRegimenChangeDate,
                                    SecondlineRegimenChangeReason = @SecondlineRegimenChangeReason,
                                    Adherence = @Adherence,
                                    AdherenceCategory = @AdherenceCategory,
                                    FamilyPlanningMethod = @FamilyPlanningMethod,
                                    PwP = @PwP,
                                    GestationAge = @GestationAge,
                                    NextAppointmentDate = @NextAppointmentDate,
                                    StabilityAssessment = @StabilityAssessment,
                                    DifferentiatedCare = @DifferentiatedCare,
                                    PopulationType = @PopulationType,
                                    KeyPopulationType = @KeyPopulationType,
                                    VisitBy = @VisitBy,
                                    Temp = @Temp,
                                    PulseRate = @PulseRate,
                                    RespiratoryRate = @RespiratoryRate,
                                    OxygenSaturation = @OxygenSaturation,
                                    Muac = @Muac,
                                    NutritionalStatus = @NutritionalStatus,
                                    EverHadMenses = @EverHadMenses,
                                    Breastfeeding = @Breastfeeding,
                                    Menopausal = @Menopausal,
                                    NoFPReason = @NoFPReason,
                                    ProphylaxisUsed = @ProphylaxisUsed,
                                    CTXAdherence = @CTXAdherence,
                                    CurrentRegimen = @CurrentRegimen,
                                    HCWConcern = @HCWConcern,
                                    TCAReason = @TCAReason,
                                    ClinicalNotes = @ClinicalNotes,
                                    GeneralExamination = @GeneralExamination,
                                    SystemExamination = @SystemExamination,
                                    Skin = @Skin,
                                    Eyes = @Eyes,
                                    ENT = @ENT,
                                    Chest = @Chest,
                                    CVS = @CVS,
                                    Abdomen = @Abdomen,
                                    CNS = @CNS,
                                    Genitourinary = @Genitourinary,
                                    RefillDate = @RefillDate,
                                    Date_Created = @Date_Created,
                                    DateLastModified = @DateLastModified,
                                    DateExtracted = @DateExtracted,
                                    Created = @Created,
                                    Updated = @Updated,
                                    Voided = @Voided                          

                             WHERE  RecordUUID = @RecordUUID";

                               

                                    await connection.ExecuteAsync(sql, recordsToUpdate, transaction);
                                    transaction.Commit();

                                break; 
                            }
                            catch (SqlException ex)
                            {
                                if (ex.Number == 1205) 
                                {

                                    await Task.Delay(100);
                                }
                                else
                                {
                                    transaction.Rollback();
                                    throw; 
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
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



