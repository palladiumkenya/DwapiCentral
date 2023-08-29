using Dapper;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class PatientStatusRepository : IPatientStatusRepository
    {
        private readonly CtDbContext _context;

        public PatientStatusRepository(CtDbContext context)
        {
            _context = context;
        }
        public Task MergeAsync(IEnumerable<PatientStatusExtract> patientStatusExtracts)
        {
            

            var distinctExtracts = patientStatusExtracts
            .GroupBy(e => new { e.PatientPk, e.SiteCode, e.ExitDate, e.ExitReason,e.TOVerifiedDate })
            .Select(g => g.OrderByDescending(e => e.Id).First())
            .ToList();

            var existingExtracts = _context.PatientStatusExtract
                .AsEnumerable()
                .Where(e => distinctExtracts.Any(d =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.ExitDate == e.ExitDate &&
                    d.ExitReason == e.ExitReason &&
                    d.TOVerifiedDate == e.TOVerifiedDate))
                .ToList();

            var distinctToInsert = distinctExtracts
                .Where(d => !existingExtracts.Any(e =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.ExitDate == e.ExitDate &&
                    d.ExitReason == e.ExitReason &&
                    d.TOVerifiedDate == e.TOVerifiedDate))
                .ToList();


            _context.Database.GetDbConnection().BulkMerge(distinctToInsert);


            _context.SaveChangesAsync();

            return Task.CompletedTask;

           
           
        }

    }
}
