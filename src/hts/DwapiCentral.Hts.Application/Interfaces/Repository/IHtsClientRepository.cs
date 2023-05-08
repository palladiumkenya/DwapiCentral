using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Shared.Application.Interfaces.Repository.Common;

namespace DwapiCentral.Hts.Application.Interfaces.Repository.Hts
{
    public interface IHtsClientRepository : IRepository<HtsClient,Guid>
    {
        void Process(Guid facilityId, IEnumerable<HtsClient> clients);
    }
}
