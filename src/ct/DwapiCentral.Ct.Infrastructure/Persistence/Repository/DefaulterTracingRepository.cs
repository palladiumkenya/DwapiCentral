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

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class DefaulterTracingRepository : IDefaulterTracingRepository
    {
        private readonly CtDbContext _context;

        public DefaulterTracingRepository(CtDbContext context)
        {
            _context = context;
        }
        public Task MergeAsync(IEnumerable<DefaulterTracingExtract> defaulterTracingExtracts)
        {
            var distinctExtracts = defaulterTracingExtracts
               .GroupBy(e => new { e.PatientPk, e.SiteCode, e.VisitID, e.VisitDate })
               .Select(g => g.OrderByDescending(e => e.Id).First()).ToList();

            var existingExtracts = _context.DefaulterTracingExtract
                 .AsEnumerable()
                 .Where(e => distinctExtracts.Any(d =>
                     d.PatientPk == e.PatientPk &&
                     d.SiteCode == e.SiteCode &&
                     d.VisitID == e.VisitID &&
                     d.VisitDate == e.VisitDate
                    ))
                 .ToList();

            var distinctToInsert = distinctExtracts
                .Where(d => !existingExtracts.Any(e =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.VisitID == e.VisitID &&
                    d.VisitDate == e.VisitDate))
                .ToList();

            _context.Database.GetDbConnection().BulkMerge(distinctToInsert);

            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
