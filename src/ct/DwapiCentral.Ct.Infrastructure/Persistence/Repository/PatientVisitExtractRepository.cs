using Dapper;
using DwapiCentral.Contracts.Common;
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
using System.Data;
using System.Linq;
using Z.Dapper.Plus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository;

public class PatientVisitExtractRepository:IPatientVisitExtractRepository
{
    private readonly CtDbContext _context;
    private readonly IMediator _mediator;
    private readonly IManifestRepository _manifestRepository;

    public PatientVisitExtractRepository(CtDbContext context, IMediator mediator, IManifestRepository manifestRepository)
    {
        _context = context;
        _mediator = mediator;
        _manifestRepository = manifestRepository;
    }

    public async Task<PatientVisitExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID)
    {
        // If not cached, retrieve the record from the database
        var query = "SELECT * FROM PatientVisitExtract WHERE PatientPK = @PatientPK AND SiteCode = @SiteCode AND RecordUUID = @RecordUUID";

        var patientExtract = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<PatientVisitExtract>(query, new { patientPK, siteCode, recordUUID });

        return patientExtract;

    }

    public async Task InsertExtract(List<PatientVisitExtract> patientExtract)
    {
        try
        {
            var cons = _context.Database.GetConnectionString();
            using var connection = new SqlConnection(cons);
            if (connection.State != ConnectionState.Open)
                connection.Open();

            _context.Database.GetDbConnection().BulkInsert(patientExtract);

            var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

            var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "PatientVisitExtract" };
            await _mediator.Publish(notification);

            connection.Close();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred during bulk Insert of PatientVisitExtract.");
        }
    }

    public async Task UpdateExtract(List<PatientVisitExtract> patientExtract)
    {
        try
        {
            var cons = _context.Database.GetConnectionString();
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

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND RecordUUID = @RecordUUID";



            using var connection = new SqlConnection(cons);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            await connection.ExecuteAsync(sql, patientExtract);

            var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

            connection.Close();

            var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "PatientVisitExtract", UploadMode = UploadMode.DifferentialLoad };
            await _mediator.Publish(notification);


        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred during bulk update of PatientVisitExtract.");

        }
    }
}
