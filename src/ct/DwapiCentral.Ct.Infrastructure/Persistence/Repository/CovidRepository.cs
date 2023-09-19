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

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository
{
    public class CovidRepository : ICovidRepository
    {
        private readonly CtDbContext _context;
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public CovidRepository(CtDbContext context, IMediator mediator, IManifestRepository manifestRepository)
        {
            _context = context;
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        public async Task<CovidExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID)
        {
            // If not cached, retrieve the record from the database
            var query = "SELECT * FROM CovidExtract WHERE PatientPK = @PatientPK AND SiteCode = @SiteCode AND RecordUUID = @RecordUUID";

            var patientExtract = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<CovidExtract>(query, new { patientPK, siteCode, recordUUID });

            return patientExtract;

        }

        public async Task InsertExtract(List<CovidExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                _context.Database.GetDbConnection().BulkInsert(patientExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "CovidExtract" };
                await _mediator.Publish(notification);

                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk Insert of CovidExtract.");
            }
        }

        public async Task UpdateExtract(List<CovidExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();


                var sql = $@"
                           UPDATE 
                                     CovidExtract

                               SET  VisitID = @VisitID,
                                    Covid19AssessmentDate = @Covid19AssessmentDate,                                   
                                    ReceivedCOVID19Vaccine = @ReceivedCOVID19Vaccine,                                    
                                    FirstDoseVaccineAdministered = @FirstDoseVaccineAdministered,                                    
                                    SecondDoseVaccineAdministered = @SecondDoseVaccineAdministered,
                                    VaccinationStatus = @VaccinationStatus,
                                    VaccineVerification = @VaccineVerification,
                                    BoosterGiven = @BoosterGiven,
                                    BoosterDose = @BoosterDose,                                    
                                    EverCOVID19Positive = @EverCOVID19Positive,
                                    COVID19TestDate = COALESCE(@COVID19TestDate, 1900-01-01),
                                    PatientStatus = @PatientStatus,
                                    AdmissionStatus = @AdmissionStatus,
                                    AdmissionUnit = @AdmissionUnit,
                                    MissedAppointmentDueToCOVID19 = @MissedAppointmentDueToCOVID19,                                    
                                    COVID19TestDateSinceLastVisit = COALESCE(@COVID19TestDateSinceLastVisit,1900-01-01),
                                    PatientStatusSinceLastVisit = @PatientStatusSinceLastVisit,
                                    AdmissionStatusSinceLastVisit = @AdmissionStatusSinceLastVisit,                                   
                                    AdmissionUnitSinceLastVisit = @AdmissionUnitSinceLastVisit,
                                    SupplementalOxygenReceived = @SupplementalOxygenReceived,
                                    PatientVentilated = @PatientVentilated,
                                    TracingFinalOutcome = @TracingFinalOutcome,
                                    CauseOfDeath = @CauseOfDeath,
                                    COVID19TestResult = @COVID19TestResult,
                                    Sequence = @Sequence,
                                    BoosterDoseVerified = @BoosterDoseVerified,
                                    Date_Created = @Date_Created,
                                    DateLastModified = COALESCE(@DateLastModified,1900-01-01),
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

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "CovidExtract", UploadMode = UploadMode.DifferentialLoad };
                await _mediator.Publish(notification);


            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk update of CovidExtract.");

            }
        }
    }
}
