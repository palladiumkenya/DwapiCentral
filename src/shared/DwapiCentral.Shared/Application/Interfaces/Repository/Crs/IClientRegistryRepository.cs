using DwapiCentral.Shared.Application.Interfaces.Repository.Common;
using DwapiCentral.Shared.Domain.Model.Crs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Repository.Crs
{
    public interface IClientRegistryRepository : IRepository<ClientRegistry,Guid>
    {
        void Process(Guid facilityId, IEnumerable<ClientRegistry> clients);
    }
}
