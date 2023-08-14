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
    public class FacilityRepository : IFacilityRepository
    {
        private readonly HtsDbContext _context;

        public FacilityRepository(HtsDbContext context)
        {
            _context = context;
        }

        public async Task<Facility?> GetByCode(int code)
        {
            return await _context.Facilities
               .AsTracking()
               .FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task Save(Facility facility)
        {
            await _context.Facilities.AddAsync(facility);
            await _context.SaveChangesAsync();
        }
    }
}
