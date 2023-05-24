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
            _context.Database.GetDbConnection().BulkMerge(patientArtExtracts);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
