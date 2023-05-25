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
    public class PatientStatusRepository : IPatientStatusRepository
    {
        private readonly CtDbContext _context;

        public PatientStatusRepository(CtDbContext context)
        {
            _context = context;
        }
        public Task MergeAsync(IEnumerable<PatientStatusExtract> patientStatusExtracts)
        {
            _context.Database.GetDbConnection().BulkMerge(patientStatusExtracts);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

    }
}
