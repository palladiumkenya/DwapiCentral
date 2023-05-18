using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository;


public class MasterFacilityRepository:IMasterFacilityRepository
{
    private readonly CtDbContext _context;

    public MasterFacilityRepository(CtDbContext context)
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