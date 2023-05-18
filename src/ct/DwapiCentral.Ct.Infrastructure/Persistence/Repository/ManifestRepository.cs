using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository;

public class ManifestRepository:IManifestRepository
{
    public Task<Manifest> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Save(Manifest manifest)
    {
        throw new NotImplementedException();
    }

    public Task Update(Manifest manifest)
    {
        throw new NotImplementedException();
    }
}