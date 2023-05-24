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

namespace DwapiCentral.Ct.Infrastructure.Tests.Persistence.Repository
{
    public class CovidRepository : ICovidRepository
    {
        private readonly CtDbContext _context;

        public CovidRepository(CtDbContext context)
        {
            _context = context;
        }
        public Task MergeAsync(IEnumerable<CovidExtract> covidExtracts)
        {
            _context.Database.GetDbConnection().BulkMerge(covidExtracts);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
