using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;

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
        throw new NotImplementedException();
    }

    public Task Save(Facility facility)
    {
        throw new NotImplementedException();
    }
}