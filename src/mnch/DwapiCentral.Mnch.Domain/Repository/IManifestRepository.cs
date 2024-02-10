using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Shared.Domain.Model.Common;
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
        Task Save(Cargo cargo);
        Task Update(Manifest manifest);
        Task ClearFacility(int siteCode);
        Task ClearFacility(int siteCode, string project);
        int GetPatientCount(Guid id);
        IEnumerable<Manifest> GetStaged(int sitecode);
        void updateCount(Guid id, int clientCount);

    }
}
