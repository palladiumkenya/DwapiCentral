using Dapper;
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

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class PatientBaseLinesRepository : IPatientBaseLinesRepository
    {
        private readonly CtDbContext _context;
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public PatientBaseLinesRepository(CtDbContext context, IMediator mediator, IManifestRepository manifestRepository)
        {
            _context = context;
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        public async Task<PatientBaselinesExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID)
        {
            // If not cached, retrieve the record from the database
            var query = "SELECT * FROM PatientBaselinesExtract WHERE PatientPK = @PatientPK AND SiteCode = @SiteCode AND RecordUUID = @RecordUUID";

            var patientExtract = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<PatientBaselinesExtract>(query, new { patientPK, siteCode, recordUUID });

            return patientExtract;

        }

        public async Task InsertExtract(List<PatientBaselinesExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                _context.Database.GetDbConnection().BulkInsert(patientExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "PatientBaselineExtract" };
                await _mediator.Publish(notification);

                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk Insert of PatientBaselineExtract.");
            }
        }

        public async Task UpdateExtract(List<PatientBaselinesExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();

                var sql = $@"
                           UPDATE 
                                     PatientBaselinesExtract

                               SET     
                                                                        
                                    bCD4Date = @bCD4Date,
                                    bWAB = @bWAB,
                                    bWABDate = @bWABDate,
                                    bWHO = @bWHO,
                                    bWHODate = @bWHODate,
                                    eWAB = @eWAB,
                                    eWABDate = @eWABDate,
                                    eCD4 = @eCD4,
                                    eCD4Date = @eCD4Date,
                                    eWHO = @eWHO,
                                    eWHODate = @eWHODate,
                                    lastWHO = @lastWHO,
                                    lastWHODate = @lastWHODate,
                                    lastCD4 = @lastCD4,
                                    lastCD4Date = @lastCD4Date,
                                    lastWAB = @lastWAB,
                                    lastWABDate = @lastWABDate,
                                    m12CD4 = @m12CD4,
                                    m12CD4Date = @m12CD4Date,
                                    m6CD4 = @m6CD4,
                                    m6CD4Date = @m6CD4Date,
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

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "PatientBaselineExtract" };
                await _mediator.Publish(notification);


            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk update of PatientBaselineExtract.");

            }
        }

    }
}
