using Dapper;
using DwapiCentral.Contracts.Common;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class PatientLaboratoryExtractRepository : IPatientLaboratoryExtractRepository
    {
        private readonly CtDbContext _context;
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public PatientLaboratoryExtractRepository(CtDbContext context, IMediator mediator,IManifestRepository manifestRepository)
        {
            _context = context;
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        public async Task<PatientLaboratoryExtract> GetPatientLabExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID)
        {
            // If not cached, retrieve the record from the database
            var query = "SELECT * FROM PatientLaboratoryExtract WHERE PatientPK = @PatientPK AND SiteCode = @SiteCode AND RecordUUID = @RecordUUID";

            var patientLabExtract = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<PatientLaboratoryExtract>(query, new { patientPK, siteCode, recordUUID });

            return patientLabExtract;

        }

        public async Task InsertPatientLabExtract(List<PatientLaboratoryExtract> patientLabExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                _context.Database.GetDbConnection().BulkInsert(patientLabExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientLabExtract.First().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientLabExtract.Count, ManifestId=manifestId, SiteCode = patientLabExtract.First().SiteCode, ExtractName = "PatientLabExtract" };
                await _mediator.Publish(notification);

                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk Insert of PatientLabExtract.");
            }
        }

        public async Task UpdatePatientLabExtract(List<PatientLaboratoryExtract> patientLabExtract)
        {
            try
            {
                var cons =  _context.Database.GetConnectionString();               
               

                var sql = $@"
                           UPDATE 
                                     PatientLaboratoryExtract

                               SET
                                    VisitID = @VisitID,
                                    OrderedByDate = @OrderedByDate,
                                    ReportedByDate = @ReportedByDate,
                                    TestName = @TestName,
                                    EnrollmentTest = @EnrollmentTest,
                                    TestResult = @TestResult,
                                    DateSampleTaken = @DateSampleTaken,
                                    SampleType = @SampleType,
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
                await connection.ExecuteAsync(sql, patientLabExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientLabExtract.First().SiteCode);

                connection.Close();

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientLabExtract.Count, ManifestId = manifestId, SiteCode = patientLabExtract.First().SiteCode, ExtractName = "PatientLabExtract" };
                await _mediator.Publish(notification);

                
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk update of PatientLabExtract.");
          
            }
        }
    }
}
