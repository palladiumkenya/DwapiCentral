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
            _context.Database.GetDbConnection().BulkMerge(contactListingExtracts);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
