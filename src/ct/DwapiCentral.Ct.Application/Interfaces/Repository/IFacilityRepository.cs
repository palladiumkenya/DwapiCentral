using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{
    public interface IFacilityRepository 
    {
        Task<Facility> GetByCode(int code);
        Task Save(Facility facility);
    }
}

