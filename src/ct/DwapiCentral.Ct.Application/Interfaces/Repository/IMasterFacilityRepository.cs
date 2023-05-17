using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Application.Interfaces.Repository;

public interface IMasterFacilityRepository
{
    Task<MasterFacility> GetByCode(int code);
}