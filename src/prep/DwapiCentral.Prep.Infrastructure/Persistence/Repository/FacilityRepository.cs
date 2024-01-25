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
    public class FacilityRepository : IFacilityRepository
    {
        private readonly PrepDbContext _context;

        public FacilityRepository(PrepDbContext context)
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
