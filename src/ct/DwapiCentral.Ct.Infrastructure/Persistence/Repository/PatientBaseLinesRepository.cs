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
    public class PatientBaseLinesRepository : IPatientBaseLinesRepository
    {
        private readonly CtDbContext _context;

        public PatientBaseLinesRepository(CtDbContext context)
        {
            _context = context;
        }
        public Task MergeAsync(IEnumerable<PatientBaselinesExtract> patientBaselinesExtracts)
        {
           var distinctExtracts = patientBaselinesExtracts
                .GroupBy(e => new { e.PatientPk, e.SiteCode, e.Date_Created })
                .Select(g => g.OrderByDescending(e => e.Id).First())
                .ToList();

            var existingExtracts = _context.PatientBaselinesExtracts
                .AsEnumerable()
                .Where(e => distinctExtracts.Any(d =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.Date_Created == e.Date_Created
                   ))
                .ToList();

            var distinctToInsert = distinctExtracts
                .Where(d => !existingExtracts.Any(e =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.Date_Created == e.Date_Created ))
                .ToList();



            _context.Database.GetDbConnection().BulkMerge(distinctToInsert);


            _context.SaveChangesAsync();

            return Task.CompletedTask;
        }

    
    }
}
