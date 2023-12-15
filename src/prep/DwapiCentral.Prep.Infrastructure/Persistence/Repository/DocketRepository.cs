using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Repository;
using DwapiCentral.Prep.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Infrastructure.Persistence.Repository
{
    public class DocketRepository : IDocketRepository
    {
        private readonly PrepDbContext _context;

        public DocketRepository(PrepDbContext context)
        {
            _context = context;
        }

        public Task<Docket?> GetDocketId(string docket)
        {
            return _context.Dockets
                   .Include(x => x.Subscribers)
                   .AsTracking()
                   .FirstOrDefaultAsync(x => x.Id == docket);
        }
    }
}
