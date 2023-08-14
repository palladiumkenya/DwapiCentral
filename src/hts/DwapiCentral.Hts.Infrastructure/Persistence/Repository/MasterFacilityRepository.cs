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
    public class MasterFacilityRepository : IMasterFacilityRepository
    {
        private readonly HtsDbContext _context;

        public MasterFacilityRepository(HtsDbContext context)
        {
            _context = context;
        }

        public Task<MasterFacility?> GetByCode(int code)
        {
            return _context.MasterFacilities
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
