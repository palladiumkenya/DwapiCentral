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
    public class PatientLaboratoryExtractRepository : IPatientLaboratoryExtractRepository
    {
        private readonly CtDbContext _context;

        public PatientLaboratoryExtractRepository(CtDbContext context)
        {
            _context = context;
        }

        public Task MergeLaboratoryExtracts(IEnumerable<PatientLaboratoryExtract> patientLabs)
        {
            var distinctExtracts = patientLabs
               .GroupBy(e => new { e.PatientPk, e.SiteCode, e.VisitId, e.OrderedByDate,e.TestResult,e.TestName })
               .Select(g => g.OrderByDescending(e => e.Id).First()).ToList();

            var existingExtracts = _context.PatientLaboratoryExtract
                 .AsEnumerable()
                 .Where(e => distinctExtracts.Any(d =>
                     d.PatientPk == e.PatientPk &&
                     d.SiteCode == e.SiteCode &&
                     d.VisitId == e.VisitId &&
                     d.OrderedByDate == e.OrderedByDate &&
                     d.TestResult ==e.TestResult &&
                     d.TestName == e.TestName
                    ))
                 .ToList();

            var distinctToInsert = distinctExtracts
                .Where(d => !existingExtracts.Any(e =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.VisitId == e.VisitId &&
                    d.OrderedByDate == e.OrderedByDate &&
                    d.TestResult == e.TestResult &&
                    d.TestName == e.TestName))
                .ToList();

            _context.Database.GetDbConnection().BulkMerge(distinctToInsert);

            _context.SaveChangesAsync();

          
           
            return Task.CompletedTask;
        }
    }
}
