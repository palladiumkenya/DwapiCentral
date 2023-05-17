using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository;


public class MasterFacilityRepository:IMasterFacilityRepository
{
    public Task<MasterFacility> GetByCode(int code)
    {
        throw new NotImplementedException();
    }
}