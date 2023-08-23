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
    public class MasterFacilityRepository : IMasterFacilityRepository
    {
        private readonly PrepDbContext _context;

        public MasterFacilityRepository(PrepDbContext context)
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
