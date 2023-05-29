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
            var uniqueLabExtracts = new HashSet<string>();

            foreach (var labExtract in patientLabs)
            {
                var labExtractKey = $"{labExtract.PatientPk}_{labExtract.SiteCode}_{labExtract.VisitId}_{labExtract.OrderedByDate}";
                if (uniqueLabExtracts.Contains(labExtractKey))
                {
                    // Skip duplicate lab extract
                    continue;
                }

               
                    // Add new lab extract
                     _context.Database.GetDbConnection().BulkMerge(patientLabs);
                

                
            }

             _context.SaveChangesAsync();

          
           
            return Task.CompletedTask;
        }
    }
}
