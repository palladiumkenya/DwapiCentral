using DwapiCentral.Ct.Domain.Models.Extracts;
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
            var uniquePharmacyExtracts = new List<PatientPharmacyExtract>();

            foreach (var pharmacyExtract in pharmacyExtracts)
            {
                var key = (pharmacyExtract.PatientPk, pharmacyExtract.SiteCode, pharmacyExtract.VisitID, pharmacyExtract.DispenseDate);

                if (_uniquePharmacyExtracts.Contains(key))
                {
                    _context.Database.GetDbConnection().BulkUpdate(uniquePharmacyExtracts);
                    continue;
                }

                _uniquePharmacyExtracts.Add(key);
                uniquePharmacyExtracts.Add(pharmacyExtract);
            }

            if (uniquePharmacyExtracts.Count > 0)
            {
               
                _context.Database.GetDbConnection().BulkInsert(uniquePharmacyExtracts);
            }
        
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
