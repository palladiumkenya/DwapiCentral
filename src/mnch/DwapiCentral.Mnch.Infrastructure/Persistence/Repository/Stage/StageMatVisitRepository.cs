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
using DwapiCentral.Contracts.Mnch;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;
using DwapiCentral.Mnch.Domain.Events;
using Dapper;

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Repository.Stage
{
    public class StageMatVisitRepository : IStageMatVisitRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MnchDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageMatVisitRepository(MnchDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageMatVisit)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageMatVisit> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "MatVisits" };
                await _mediator.Publish(notification);

                var pks = extracts.Select(x => x.Id).ToList();

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

        private async Task MergeExtracts(Guid manifestId, List<StageMatVisit> stageMatVisit)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageMatVisit> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*
                            FROM MatVisits p 
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT PatientPK, SiteCode, PatientMnchID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, PatientMnchID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.PatientMnchID = s.PatientMnchID                                   
                                                                   
                            )
                        ";

                var existingRecords = await connection.QueryAsync<MatVisit>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string PatientMnchID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.PatientMnchID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageMatVisit
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.PatientMnchID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageMatVisit, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageMatVisit;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageMatVisit> uniqueStageExtracts)
        {
            try
            {

                var latestRecordsDict = new Dictionary<string, StageMatVisit>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.PatientMnchID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<MatVisit>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageMatVisit> stageDrug, IEnumerable<MatVisit> existingRecords)
        {
            try
            {
                //Update existing data
                var stageDictionary = stageDrug
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.PatientMnchID })
                         .ToDictionary(
                             g => g.Key,
                             g => g.FirstOrDefault()
                         );

                foreach (var existingExtract in existingRecords)
                {
                    if (stageDictionary.TryGetValue(
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.PatientMnchID },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }

                var cons = _context.Database.GetConnectionString();
                var sql = $@"
                           UPDATE 
                                     MatVisits

                               SET                                  
                                    DateExtracted = @DateExtracted,                                   
                                    FacilityName = @FacilityName,
                                    VisitID = @VisitID,
                                    VisitDate = @VisitDate,
                                    AdmissionNumber = @AdmissionNumber,
                                    ANCVisits = @ANCVisits,
                                    DateOfDelivery = @DateOfDelivery,
                                    DurationOfDelivery = @DurationOfDelivery,
                                    GestationAtBirth = @GestationAtBirth,
                                    ModeOfDelivery = @ModeOfDelivery,
                                    PlacentaComplete = @PlacentaComplete,
                                    UterotonicGiven = @UterotonicGiven,
                                    VaginalExamination = @VaginalExamination,
                                    BloodLoss = @BloodLoss,
                                    BloodLossVisual = @BloodLossVisual,
                                    ConditonAfterDelivery = @ConditonAfterDelivery,
                                    MaternalDeath = @MaternalDeath,
                                    DeliveryComplications = @DeliveryComplications,
                                    NoBabiesDelivered = @NoBabiesDelivered,
                                    BabyBirthNumber = @BabyBirthNumber,
                                    SexBaby = @SexBaby,
                                    BirthWeight = @BirthWeight,
                                    BirthOutcome = @BirthOutcome,
                                    BirthWithDeformity = @BirthWithDeformity,
                                    TetracyclineGiven = @TetracyclineGiven,
                                    InitiatedBF = @InitiatedBF,
                                    ApgarScore1 = @ApgarScore1,
                                    ApgarScore5 = @ApgarScore5,
                                    ApgarScore10 = @ApgarScore10,
                                    KangarooCare = @KangarooCare,
                                    ChlorhexidineApplied = @ChlorhexidineApplied,
                                    VitaminKGiven = @VitaminKGiven,
                                    StatusBabyDischarge = @StatusBabyDischarge,
                                    MotherDischargeDate = @MotherDischargeDate,
                                    SyphilisTestResults = @SyphilisTestResults,
                                    HIVStatusLastANC = @HIVStatusLastANC,
                                    HIVTestingDone = @HIVTestingDone,
                                    HIVTest1 = @HIVTest1,
                                    HIV1Results = @HIV1Results,
                                    HIVTest2 = @HIVTest2,
                                    HIV2Results = @HIV2Results,
                                    HIVTestFinalResult = @HIVTestFinalResult,
                                    OnARTANC = @OnARTANC,
                                    BabyGivenProphylaxis = @BabyGivenProphylaxis,
                                    MotherGivenCTX = @MotherGivenCTX,
                                    PartnerHIVTestingMAT = @PartnerHIVTestingMAT,
                                    PartnerHIVStatusMAT = @PartnerHIVStatusMAT,
                                    CounselledOn = @CounselledOn,
                                    ReferredFrom = @ReferredFrom,
                                    ReferredTo = @ReferredTo,
                                    ClinicalNotes = @ClinicalNotes,                                    
                                    EDD = @EDD,
                                    LMP = @LMP,
                                    MaternalDeathAudited = @MaternalDeathAudited,
                                    OnARTMat = @OnARTMat,
                                    ReferralReason = @ReferralReason,
                                    Date_Created = @Date_Created,
                                    Date_Last_Modified = @Date_Last_Modified,
                                    Created = @Created,
                                    Updated = @Updated,
                                    Voided = @Voided         

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND PatientMnchID = @PatientMnchID";

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
