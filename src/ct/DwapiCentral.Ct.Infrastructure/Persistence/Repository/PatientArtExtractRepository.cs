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
               .GroupBy(e => new { e.PatientPk, e.SiteCode, e.LastVisit })
               .Select(g => g.OrderByDescending(e => e.Id).First());

            _context.Database.GetDbConnection().BulkMerge(distinctExtracts); 

            var extractIdsToKeep = distinctExtracts.Select(e => e.Id).ToList();
            var deleteQuery = $@"
                    DELETE FROM PatientArtExtracts
                    WHERE Id NOT IN ({string.Join(",", extractIdsToKeep)})
                ";

            _context.Database.GetDbConnection().ExecuteAsync(deleteQuery);
            
           
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
