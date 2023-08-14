using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Hts.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Infrastructure.Persistence.Repository
{
    public class DocketRepository : IDocketRepository
    {
        private readonly HtsDbContext _context;

        public DocketRepository(HtsDbContext context)
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
