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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class PatientArtExtractRepository : IPatientArtExtractRepository
    {
        private readonly CtDbContext _context;
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public PatientArtExtractRepository(CtDbContext context, IMediator mediator, IManifestRepository manifestRepository)
        {
            _context = context;
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        public async Task<PatientArtExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID)
        {
            // If not cached, retrieve the record from the database
            var query = "SELECT * FROM PatientArtExtract WHERE PatientPK = @PatientPK AND SiteCode = @SiteCode AND RecordUUID = @RecordUUID";

            var patientExtract = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<PatientArtExtract>(query, new { patientPK, siteCode, recordUUID });

            return patientExtract;

        }

        public async Task InsertExtract(List<PatientArtExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                _context.Database.GetDbConnection().BulkInsert(patientExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "PatientArtExtract" };
                await _mediator.Publish(notification);

                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk Insert of PatientArtExtract.");
            }
        }

        public async Task UpdateExtract(List<PatientArtExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();


                var sql = $@"
                           UPDATE 
                                     PatientArtExtract

                               SET     
                                     LastARTDate  = @LastARTDate
                                    ,LastVisit  = @LastVisit
                                    ,DOB  = @DOB
                                    ,AgeEnrollment  = @AgeEnrollment
                                    ,AgeARTStart  = @AgeARTStart
                                    ,AgeLastVisit  = @AgeLastVisit
                                    ,RegistrationDate  = @RegistrationDate
                                    ,Gender  = @Gender
                                    ,PatientSource  = @PatientSource
                                    ,StartARTDate  = @StartARTDate
                                    ,PreviousARTStartDate  = @PreviousARTStartDate
                                    ,PreviousARTRegimen  = @PreviousARTRegimen
                                    ,StartARTAtThisFacility  = @StartARTAtThisFacility
                                    ,StartRegimen  = @StartRegimen
                                    ,StartRegimenLine  = @StartRegimenLine
                                    ,LastRegimen  = @LastRegimen
                                    ,LastRegimenLine  = @LastRegimenLine
                                    ,Duration  = @Duration
                                    ,ExpectedReturn  = @ExpectedReturn
                                    ,Provider  = @Provider
                                    ,ExitReason  = @ExitReason
                                    ,ExitDate  = @ExitDate
                                    ,PreviousARTUse  = @PreviousARTUse
                                    ,PreviousARTPurpose  = @PreviousARTPurpose
                                    ,DateLastUsed  = @DateLastUsed
                                    ,Date_Created = @Date_Created
                                    ,DateLastModified = @DateLastModified
                                    ,DateExtracted = @DateExtracted
                                    ,Created = @Created
                                    ,Updated = @Updated
                                    ,Voided = @Voided                          

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND RecordUUID = @RecordUUID";



                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                await connection.ExecuteAsync(sql, patientExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

                connection.Close();

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "PatientArtExtract", UploadMode = UploadMode.DifferentialLoad };
                await _mediator.Publish(notification);


            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk update of PatientArtExtract.");

            }
        }
    }
}
