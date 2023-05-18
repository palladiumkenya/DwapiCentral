using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Application.Interfaces.Repository;

public interface IManifestRepository 
{
    Task<Manifest> GetById(Guid id);
    Task Save(Manifest manifest);
    Task Update(Manifest manifest);
}