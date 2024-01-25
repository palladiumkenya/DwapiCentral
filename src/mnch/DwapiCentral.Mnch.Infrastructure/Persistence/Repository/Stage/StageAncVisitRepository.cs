using AutoMapper;
using Dapper;
using DwapiCentral.Mnch.Domain.Events;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Model.Stage;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using DwapiCentral.Mnch.Infrastructure.Persistence.Context;
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

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Repository.Stage
{
    public class StageAncVisitRepository : IStageAncVisitRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MnchDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageAncVisitRepository(MnchDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageAncVisit)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageAncVisit> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);               

                var pks = extracts.Select(x => x.Id).ToList();

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "AncVisitExtract" };
                await _mediator.Publish(notification);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageAncVisit> stageAncVisit)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageAncVisit> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*
                            FROM AncVisits p 
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

                var existingRecords = await connection.QueryAsync<AncVisit>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageAncVisit
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageAncVisit, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageAncVisit;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageAncVisit> uniqueStageExtracts)
        {
            try
            {
                var latestRecordsDict = new Dictionary<string, StageAncVisit>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<AncVisit>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageAncVisit> stageDrug, IEnumerable<AncVisit> existingRecords)
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
                //                     AncVisits

                //               SET                                  
                //                    VisitID = @VisitID,
                //                    VisitDate = @VisitDate,
                //                    ANCClinicNumber = @ANCClinicNumber,
                //                    ANCVisitNo = @ANCVisitNo,
                //                    GestationWeeks = @GestationWeeks,
                //                    Height = @Height,
                //                    Weight = @Weight,
                //                    Temp = @Temp,
                //                    PulseRate = @PulseRate,
                //                    RespiratoryRate = @RespiratoryRate,
                //                    OxygenSaturation = @OxygenSaturation,
                //                    MUAC = @MUAC,
                //                    BP = @BP,
                //                    BreastExam = @BreastExam,
                //                    AntenatalExercises = @AntenatalExercises,
                //                    FGM = @FGM,
                //                    FGMComplications = @FGMComplications,
                //                    Haemoglobin = @Haemoglobin,
                //                    DiabetesTest = @DiabetesTest,
                //                    TBScreening = @TBScreening,
                //                    CACxScreen = @CACxScreen,
                //                    CACxScreenMethod = @CACxScreenMethod,
                //                    WHOStaging = @WHOStaging,
                //                    VLSampleTaken = @VLSampleTaken,
                //                    VLDate = @VLDate,
                //                    VLResult = @VLResult,
                //                    SyphilisTreatment = @SyphilisTreatment,
                //                    HIVStatusBeforeANC = @HIVStatusBeforeANC,
                //                    HIVTestingDone = @HIVTestingDone,
                //                    HIVTestType = @HIVTestType,
                //                    HIVTest1 = @HIVTest1,
                //                    HIVTest1Result = @HIVTest1Result,
                //                    HIVTest2 = @HIVTest2,
                //                    HIVTest2Result = @HIVTest2Result,
                //                    HIVTestFinalResult = @HIVTestFinalResult,
                //                    SyphilisTestDone = @SyphilisTestDone,
                //                    SyphilisTestType = @SyphilisTestType,
                //                    SyphilisTestResults = @SyphilisTestResults,
                //                    SyphilisTreated = @SyphilisTreated,
                //                    MotherProphylaxisGiven = @MotherProphylaxisGiven,
                //                    MotherGivenHAART = @MotherGivenHAART,
                //                    AZTBabyDispense = @AZTBabyDispense,
                //                    NVPBabyDispense = @NVPBabyDispense,
                //                    ChronicIllness = @ChronicIllness,
                //                    CounselledOn = @CounselledOn,
                //                    PartnerHIVTestingANC = @PartnerHIVTestingANC,
                //                    PartnerHIVStatusANC = @PartnerHIVStatusANC,
                //                    PostParturmFP = @PostParturmFP,
                //                    Deworming = @Deworming,
                //                    MalariaProphylaxis = @MalariaProphylaxis,
                //                    TetanusDose = @TetanusDose,
                //                    IronSupplementsGiven = @IronSupplementsGiven,
                //                    ReceivedMosquitoNet = @ReceivedMosquitoNet,
                //                    PreventiveServices = @PreventiveServices,
                //                    UrinalysisVariables = @UrinalysisVariables,
                //                    ReferredFrom = @ReferredFrom,
                //                    ReferredTo = @ReferredTo,
                //                    ReferralReasons = @ReferralReasons,
                //                    NextAppointmentANC = @NextAppointmentANC,
                //                    ClinicalNotes = @ClinicalNotes,
                //                    Date_Created = @Date_Created,
                //                    Date_Last_Modified = @Date_Last_Modified,
                //                    HepatitisBScreening = @HepatitisBScreening,
                //                    MiminumPackageOfCareReceived = @MiminumPackageOfCareReceived,
                //                    MiminumPackageOfCareServices = @MiminumPackageOfCareServices,
                //                    PresumptiveTreatmentDose = @PresumptiveTreatmentDose,
                //                    PresumptiveTreatmentGiven = @PresumptiveTreatmentGiven,
                //                    TreatedHepatitisB = @TreatedHepatitisB,                                   
                //                    DateLastModified = @DateLastModified,
                //                    DateExtracted = @DateExtracted,
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
