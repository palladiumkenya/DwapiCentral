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

                var existingLabExtract =  _context.PatientLaboratoryExtracts
                               .FirstOrDefaultAsync(p => p.PatientPk == labExtract.PatientPk
                                   && p.SiteCode == labExtract.SiteCode
                                   && p.VisitId == labExtract.VisitId
                                   && p.OrderedByDate == labExtract.OrderedByDate);

                if (existingLabExtract != null)
                {
                    // Update existing lab extract
                    
                }
                else
                {
                    // Add new lab extract
                     _context.PatientLaboratoryExtracts.AddAsync(labExtract);
                }

                uniqueLabExtracts.Add(labExtractKey);
            }

             _context.SaveChangesAsync();

           // _context.Database.GetDbConnection().BulkMerge(patientLabs);
           
            return Task.CompletedTask;
        }
    }
}
