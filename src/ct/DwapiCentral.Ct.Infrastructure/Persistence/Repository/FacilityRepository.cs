using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository;

public class FacilityRepository:IFacilityRepository
{
    public Task<Facility> GetByCode(int code)
    {
        throw new NotImplementedException();
    }

    public Task Save(Facility facility)
    {
        throw new NotImplementedException();
    }
}