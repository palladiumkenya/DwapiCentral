using Dapper;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class PatientPharmacyRepository : IPatientPharmacyRepository
    {
        private readonly CtDbContext _context;
        private readonly HashSet<(int PatientPk, int SiteCode, int VisitId, DateTime DispenseDate)> _uniquePharmacyExtracts;

        public PatientPharmacyRepository(CtDbContext context)
        {
            _context = context;
            _uniquePharmacyExtracts = new HashSet<(int, int, int, DateTime)>();
        }

        public Task MergePharmacyExtractsAsync(IEnumerable<PatientPharmacyExtract> pharmacyExtracts)
        {
            var distinctExtracts = pharmacyExtracts
                .GroupBy(e => new { e.PatientPk, e.SiteCode, e.VisitID, e.DispenseDate ,e.Created})
                .Select(g => g.OrderByDescending(e => e.Id).First())
                .ToList();

            var existingExtracts = _context.PatientPharmacyExtract
                .AsEnumerable()
                .Where(e => distinctExtracts.Any(d =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.VisitID == e.VisitID &&
                    d.DispenseDate == e.DispenseDate &&
                    d.Created == e.Created))
                .ToList();

            var distinctToInsert = distinctExtracts
                .Where(d => !existingExtracts.Any(e =>
                    d.PatientPk == e.PatientPk &&
                    d.SiteCode == e.SiteCode &&
                    d.VisitID == e.VisitID &&
                    d.DispenseDate == e.DispenseDate &&
                    d.Created == e.Created
                    ))
                .ToList();



            _context.Database.GetDbConnection().BulkMerge(distinctToInsert);





            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
