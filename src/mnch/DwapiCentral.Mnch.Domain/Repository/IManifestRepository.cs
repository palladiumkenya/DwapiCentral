using DwapiCentral.Mnch.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Repository
{
    public interface IManifestRepository
    {
        Task<Manifest> GetById(Guid id);

        Task<Guid> GetManifestId(int siteCode);

        Task Save(Manifest manifest);
        Task Update(Manifest manifest);
        Task ClearFacility(int siteCode);
        Task ClearFacility(int siteCode, string project);
    }
}
