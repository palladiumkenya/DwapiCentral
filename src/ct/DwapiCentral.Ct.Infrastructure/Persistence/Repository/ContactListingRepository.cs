using Dapper;
using DwapiCentral.Contracts.Ct;
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
    public class ContactListingRepository : IContactListingRepository
    {
        private readonly CtDbContext _context;

        public ContactListingRepository(CtDbContext context)
        {
            _context = context;
        }
        public Task MergeAsync(IEnumerable<ContactListingExtract> contactListingExtracts)
        {
            var distinctExtracts = contactListingExtracts
               .GroupBy(e => new { e.PatientPk, e.SiteCode, e.Date_Created })
               .Select(g => g.OrderByDescending(e => e.Id).First()).ToList();        

            var existingExtracts = _context.ContactListingExtract
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
                    d.Date_Created == e.Date_Created))
                .ToList();

            _context.Database.GetDbConnection().BulkMerge(distinctToInsert);



            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
