using DwapiCentral.Shared.Application.Interfaces.Repository.Common;
using DwapiCentral.Shared.Domain.Model.Hts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Repository.Hts
{
    public interface IHtsHtsTestKitsRepository : IRepository<HtsTestKits,Guid>
    {
        void Process(Guid facilityId, IEnumerable<HtsTestKits> clients);
    }
}
