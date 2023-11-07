using AutoMapper;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
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
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;
using DwapiCentral.Ct.Domain.Events;
using Dapper;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageCancerScreeningExtractsRepository : IStageCancerScreeningExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageCancerScreeningExtractsRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageCancerScreeningExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageCancerScreeningExtract> extracts, Guid manifestId)
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

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "CancerScreeningExtract" };

                await _mediator.Publish(notification);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageCancerScreeningExtract> stageCancerScreening)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageCancerScreeningExtract> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Assigned
                };
                var query = $@"
                            SELECT p.*
                            FROM CancerScreeningExtract p
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

                var existingRecords = await connection.QueryAsync<CancerScreeningExtract>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageCancerScreening
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID)) && x.LiveSession == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageCancerScreening, existingRecords);
                }
                else
                {
                    uniqueStageExtracts = stageCancerScreening;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageCancerScreeningExtract> uniqueStageExtracts)
        {
            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageCancerScreeningExtract>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<CancerScreeningExtract>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageCancerScreeningExtract> stageCancerScreening, IEnumerable<CancerScreeningExtract> existingRecords)
        {
            try
            {

                var centraldata = stageCancerScreening.Select(_mapper.Map<StageCancerScreeningExtract, CancerScreeningExtract>).ToList();


                var existingIds = existingRecords.Select(x => x.RecordUUID).ToHashSet();


                var recordsToUpdate = centraldata.Where(x => existingIds.Contains(x.RecordUUID)).ToList();


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
                                     CancerScreeningExtract

                               SET     
                                                                        
                                    FacilityName = @FacilityName,
                                    VisitID = @VisitID,
                                    VisitDate = @VisitDate,
                                    VisitType = @VisitType,
                                    ScreeningMethod = @ScreeningMethod,
                                    TreatmentToday = @TreatmentToday,
                                    ReferredOut = @ReferredOut,
                                    NextAppointmentDate = @NextAppointmentDate,
                                    ScreeningType = @ScreeningType,
                                    ScreeningResult = @ScreeningResult,
                                    PostTreatmentComplicationCause = @PostTreatmentComplicationCause,
                                    OtherPostTreatmentComplication = @OtherPostTreatmentComplication,
                                    ReferralReason = @ReferralReason,
                                    SmokesCigarette = @SmokesCigarette,
                                    NumberYearsSmoked = @NumberYearsSmoked,
                                    NumberCigarettesPerDay = @NumberCigarettesPerDay,
                                    OtherFormTobacco = @OtherFormTobacco,
                                    TakesAlcohol = @TakesAlcohol,
                                    HIVStatus = @HIVStatus,
                                    FamilyHistoryOfCa = @FamilyHistoryOfCa,
                                    PreviousCaTreatment = @PreviousCaTreatment,
                                    SymptomsCa = @SymptomsCa,
                                    CancerType = @CancerType,
                                    FecalOccultBloodTest = @FecalOccultBloodTest,
                                    TreatmentOccultBlood = @TreatmentOccultBlood,
                                    Colonoscopy = @Colonoscopy,
                                    TreatmentColonoscopy = @TreatmentColonoscopy,
                                    EUA = @EUA,
                                    TreatmentRetinoblastoma = @TreatmentRetinoblastoma,
                                    RetinoblastomaGene = @RetinoblastomaGene,
                                    TreatmentEUA = @TreatmentEUA,
                                    DRE = @DRE,
                                    TreatmentDRE = @TreatmentDRE,
                                    PSA = @PSA,
                                    TreatmentPSA = @TreatmentPSA,
                                    VisualExamination = @VisualExamination,
                                    TreatmentVE = @TreatmentVE,
                                    Cytology = @Cytology,
                                    TreatmentCytology = @TreatmentCytology,
                                    Imaging = @Imaging,
                                    TreatmentImaging = @TreatmentImaging,
                                    Biopsy = @Biopsy,
                                    TreatmentBiopsy = @TreatmentBiopsy,
                                    HPVScreeningResult = @HPVScreeningResult,
                                    TreatmentHPV = @TreatmentHPV,
                                    VIAVILIScreeningResult = @VIAVILIScreeningResult,
                                    PAPSmearScreeningResult = @PAPSmearScreeningResult,
                                    TreatmentPapSmear = @TreatmentPapSmear,
                                    ReferalOrdered = @ReferalOrdered,
                                    Colposcopy = @Colposcopy,
                                    TreatmentColposcopy = @TreatmentColposcopy,
                                    CBE = @CBE,
                                    TreatmentCBE = @TreatmentCBE,
                                    Ultrasound = @Ultrasound,
                                    TreatmentUltraSound = @TreatmentUltraSound,
                                    IfTissueDiagnosis = @IfTissueDiagnosis,
                                    DateTissueDiagnosis = @DateTissueDiagnosis,
                                    ReasonNotDone = @ReasonNotDone,
                                    Referred = @Referred,
                                    ReasonForReferral = @ReasonForReferral,                                   
                                    Date_Created = @Date_Created,
                                    DateLastModified = @DateLastModified,
                                    DateExtracted = @DateExtracted,
                                    Created = @Created,
                                    Updated = @Updated,
                                    Voided = @Voided                       

                             WHERE   RecordUUID = @RecordUUID";

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
