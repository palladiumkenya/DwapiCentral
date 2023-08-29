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

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository
{
    public class CovidRepository : ICovidRepository
    {
        private readonly CtDbContext _context;

        public CovidRepository(CtDbContext context)
        {
            _context = context;
        }
        public Task MergeAsync(IEnumerable<CovidExtract> covidExtracts)
        {
            var distinctExtracts = covidExtracts
               .GroupBy(e => new { e.PatientPk, e.SiteCode, e.VisitID, e.Covid19AssessmentDate })
               .Select(g => g.OrderByDescending(e => e.Id).First()).ToList();

            var existingExtracts = _context.CovidExtract
                 .AsEnumerable()
                 .Where(e => distinctExtracts.Any(d =>
                     d.PatientPk == e.PatientPk &&
                     d.SiteCode == e.SiteCode &&
                     d.VisitID == e.VisitID &&
                     d.Covid19AssessmentDate == e.Covid19AssessmentDate
                    ))
                 .ToList();

            var distinctToInsert = distinctExtracts
                .Where(d => !existingExtracts.Any(e =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.VisitID == e.VisitID &&
                    d.Covid19AssessmentDate == e.Covid19AssessmentDate))
                .ToList();

            _context.Database.GetDbConnection().BulkMerge(distinctToInsert);


            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
