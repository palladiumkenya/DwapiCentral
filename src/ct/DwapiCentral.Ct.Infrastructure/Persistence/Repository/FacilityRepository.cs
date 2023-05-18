using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository;

public class FacilityRepository:IFacilityRepository
{
    private readonly CtDbContext _context;

    public FacilityRepository(CtDbContext context)
    {
        _context = context;
    }

    public Task<Facility?> GetByCode(int code)
    {
        return _context.Facilities
           .AsTracking()
           .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task Save(Facility facility)
    {
        await _context.Facilities.AddAsync(facility);
        await _context.SaveChangesAsync();
    }
}