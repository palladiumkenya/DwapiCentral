using AutoMapper;
using DwapiCentral.Prep.Domain.Models.Stage;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Infrastructure.Persistence.Context;
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
using DwapiCentral.Prep.Domain.Repository.Stage;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;
using DwapiCentral.Prep.Domain.Events;
using Dapper;

namespace DwapiCentral.Prep.Infrastructure.Persistence.Repository.Stage
{
    public class StagePrepVisitRepository : IStagePrepVisitRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly PrepDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StagePrepVisitRepository(PrepDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StagePrepVisit)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StagePrepVisit> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var pks = extracts.Select(x => x.Id).ToList();

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);


                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PrepVisitExtract" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StagePrepVisit> stagePrepVisits)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StagePrepVisit> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*
                            FROM PrepVisits p 
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT DISTINCT PatientPK, SiteCode, PrepNumber, RecordUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, PrepNumber,RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.PrepNumber = s.PrepNumber  
                                    AND p.RecordUUID = s.RecordUUID
                                                                   
                            )
                        ";

                var existingRecords = await connection.QueryAsync<PrepVisit>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string PrepNumber, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.PrepNumber, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stagePrepVisits
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.PrepNumber,x.RecordUUID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stagePrepVisits, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stagePrepVisits;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StagePrepVisit> uniqueStageExtracts)
        {
            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StagePrepVisit>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.PrepNumber}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<PrepVisit>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StagePrepVisit> stageDrug, IEnumerable<PrepVisit> existingRecords)
        {
            try
            {
                //Update existing data
                var stageDictionary = stageDrug
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.PrepNumber,x.RecordUUID })
                         .ToDictionary(
                             g => g.Key,
                             g => g.FirstOrDefault()
                         );

                foreach (var existingExtract in existingRecords)
                {
                    if (stageDictionary.TryGetValue(
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.PrepNumber,existingExtract.RecordUUID },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }

                var cons = _context.Database.GetConnectionString();
                var sql = $@"
                           UPDATE 
                                     PrepVisits

                               SET                                  
                                        DateExtracted = @DateExtracted,                                       
                                        FacilityName = @FacilityName,                                       
                                        HtsNumber = @HtsNumber,
                                        EncounterId = @EncounterId,
                                        VisitID = @VisitID,
                                        VisitDate = @VisitDate,
                                        BloodPressure = @BloodPressure,
                                        Temperature = @Temperature,
                                        Weight = @Weight,
                                        Height = @Height,
                                        BMI = @BMI,
                                        STIScreening = @STIScreening,
                                        STISymptoms = @STISymptoms,
                                        STITreated = @STITreated,
                                        Circumcised = @Circumcised,
                                        VMMCReferral = @VMMCReferral,
                                        LMP = @LMP,
                                        MenopausalStatus = @MenopausalStatus,
                                        PregnantAtThisVisit = @PregnantAtThisVisit,
                                        EDD = @EDD,
                                        PlanningToGetPregnant = @PlanningToGetPregnant,
                                        PregnancyPlanned = @PregnancyPlanned,
                                        PregnancyEnded = @PregnancyEnded,
                                        PregnancyEndDate = @PregnancyEndDate,
                                        PregnancyOutcome = @PregnancyOutcome,
                                        BirthDefects = @BirthDefects,
                                        Breastfeeding = @Breastfeeding,
                                        FamilyPlanningStatus = @FamilyPlanningStatus,
                                        FPMethods = @FPMethods,
                                        AdherenceDone = @AdherenceDone,
                                        AdherenceOutcome = @AdherenceOutcome,
                                        AdherenceReasons = @AdherenceReasons,
                                        SymptomsAcuteHIV = @SymptomsAcuteHIV,
                                        ContraindicationsPrep = @ContraindicationsPrep,
                                        PrepTreatmentPlan = @PrepTreatmentPlan,
                                        PrepPrescribed = @PrepPrescribed,
                                        RegimenPrescribed = @RegimenPrescribed,
                                        MonthsPrescribed = @MonthsPrescribed,
                                        CondomsIssued = @CondomsIssued,
                                        Tobegivennextappointment = @Tobegivennextappointment,
                                        Reasonfornotgivingnextappointment = @Reasonfornotgivingnextappointment,
                                        HepatitisBPositiveResult = @HepatitisBPositiveResult,
                                        HepatitisCPositiveResult = @HepatitisCPositiveResult,
                                        VaccinationForHepBStarted = @VaccinationForHepBStarted,
                                        TreatedForHepB = @TreatedForHepB,
                                        VaccinationForHepCStarted = @VaccinationForHepCStarted,
                                        TreatedForHepC = @TreatedForHepC,
                                        NextAppointment = @NextAppointment,
                                        ClinicalNotes = @ClinicalNotes,
                                        Date_Created = @Date_Created,
                                        Date_Last_Modified = @Date_Last_Modified,
                                        Created = @Created,
                                        Updated = @Updated,
                                        Voided = @Voided
                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND PrepNumber = @PrepNumber
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
