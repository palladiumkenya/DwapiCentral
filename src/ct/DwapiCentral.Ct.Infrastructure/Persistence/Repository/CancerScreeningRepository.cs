using Dapper;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class CancerScreeningRepository : ICancerScreeningRepository
    {
        private readonly CtDbContext _context;
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public CancerScreeningRepository(CtDbContext context, IMediator mediator, IManifestRepository manifestRepository)
        {
            _context = context;
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        public async Task<CancerScreeningExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID)
        {
            // If not cached, retrieve the record from the database
            var query = "SELECT * FROM CancerScreeningExtract WHERE PatientPK = @PatientPK AND SiteCode = @SiteCode AND RecordUUID = @RecordUUID";

            var patientExtract = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<CancerScreeningExtract>(query, new { patientPK, siteCode, recordUUID });

            return patientExtract;

        }

        public async Task InsertExtract(List<CancerScreeningExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                _context.Database.GetDbConnection().BulkInsert (patientExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "CancerScreeningExtract" };
                await _mediator.Publish(notification);

                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk Insert of CancerScreeningExtract.");
            }
        }

        public async Task UpdateExtract(List<CancerScreeningExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();


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

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND RecordUUID = @RecordUUID";

                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                await connection.ExecuteAsync(sql, patientExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

                connection.Close();

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "CancerScreeningExtract", UploadMode = UploadMode.DifferentialLoad };
                await _mediator.Publish(notification);


            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk update of CancerScreeningExtract.");

            }
        }
    }
}
