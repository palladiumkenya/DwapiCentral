using DwapiCentral.Shared.Application.Interfaces.Repository.Common;
using DwapiCentral.Shared.Domain.Model.Prep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Repository.Prep
{
    public interface IPrepCareTerminationRepository :  IRepository<PrepCareTermination,Guid>
    {
        void Process(Guid facilityId, IEnumerable<PrepCareTermination> clients);
    }
}
