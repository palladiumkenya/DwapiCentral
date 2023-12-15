using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Repository;
using DwapiCentral.Mnch.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Repository
{
    public class MasterFacilityRepository : IMasterFacilityRepository
    {
        private readonly MnchDbContext _context;

        public MasterFacilityRepository(MnchDbContext context)
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
