using DwapiCentral.Prep.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Repository
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
       
        void updateCount(Guid id, int clientCount);
    }
}
