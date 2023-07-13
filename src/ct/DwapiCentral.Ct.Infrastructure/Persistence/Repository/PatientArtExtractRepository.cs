using Dapper;
using DwapiCentral.Contracts.Common;
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
    public class PatientArtExtractRepository : IPatientArtExtractRepositorycs
    {
        private readonly CtDbContext _context;

        public PatientArtExtractRepository(CtDbContext ctDbContext)
        {
            _context = ctDbContext;
        }

        public Task MergPatientArt(IEnumerable<PatientArtExtract> patientArtExtracts)
        {
            var distinctExtracts = patientArtExtracts
               .GroupBy(e => new { e.PatientPk, e.SiteCode, e.LastARTDate })
               .Select(g => g.OrderByDescending(e => e.Id).First()).ToList();


            var existingExtracts = _context.PatientArtExtracts
                .AsEnumerable()
                .Where(e => distinctExtracts.Any(d =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.LastARTDate == e.LastARTDate
                   ))
                .ToList();

            var distinctToInsert = distinctExtracts
                .Where(d => !existingExtracts.Any(e =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.LastARTDate == e.LastARTDate))
                .ToList();



            _context.Database.GetDbConnection().BulkMerge(distinctToInsert);


            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
