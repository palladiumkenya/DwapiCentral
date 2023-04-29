using DwapiCentral.Shared.Application.Interfaces.Repository.Common;
using DwapiCentral.Shared.Domain.Model.Mnch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Repository.Mnch
{
    public interface IMatVisitRepository : IRepository<MatVisit,Guid>
    {
        void Process(Guid facilityId, IEnumerable<MatVisit> clients);
    }
}
