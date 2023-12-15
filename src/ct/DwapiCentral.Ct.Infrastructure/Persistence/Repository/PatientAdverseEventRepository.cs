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
    public class PatientAdverseEventRepository : IPatientAdverseEventRepository
    {
        private readonly CtDbContext _context;
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public PatientAdverseEventRepository(CtDbContext context, IMediator mediator, IManifestRepository manifestRepository)
        {
            _context = context;
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        public async Task<PatientAdverseEventExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID)
        {
            // If not cached, retrieve the record from the database
            var query = "SELECT * FROM PatientAdverseEventExtract WHERE PatientPK = @PatientPK AND SiteCode = @SiteCode AND RecordUUID = @RecordUUID";

            var patientExtract = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<PatientAdverseEventExtract>(query, new { patientPK, siteCode, recordUUID });

            return patientExtract;

        }

        public async Task InsertExtract(List<PatientAdverseEventExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                _context.Database.GetDbConnection().BulkInsert(patientExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "PatientAdverseEventExtract" };
                await _mediator.Publish(notification);

                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk Insert of PatientAdverseEventExtract.");
            }
        }

        public async Task UpdateExtract(List<PatientAdverseEventExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();


                var sql = $@"
                           UPDATE 
                                     PatientAdverseEventExtract

                               SET     
                                      VisitDate = @VisitDate
                                      ,AdverseEvent = @AdverseEvent
                                      ,AdverseEventStartDate = @AdverseEventStartDate
                                      ,AdverseEventEndDate = @AdverseEventEndDate
                                      ,Severity = @Severity
                                      ,AdverseEventClinicalOutcome = @AdverseEventClinicalOutcome
                                      ,AdverseEventActionTaken = @AdverseEventActionTaken
                                      ,AdverseEventIsPregnant = @AdverseEventIsPregnant
                                      ,AdverseEventRegimen = @AdverseEventRegimen
                                      ,AdverseEventCause = @AdverseEventCause
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

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "PatientAdverseEventExtract", UploadMode = UploadMode.DifferentialLoad };
                await _mediator.Publish(notification);


            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk update of PatientAdverseEventExtract.");

            }
        }
    }
}
