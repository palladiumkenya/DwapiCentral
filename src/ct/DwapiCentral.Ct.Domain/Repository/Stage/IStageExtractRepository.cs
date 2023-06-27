using DwapiCentral.Shared.Application.Interfaces.Ct;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DwapiCentral.Ct.Domain.Repository.Stage
{
    public interface IStageExtractRepository<T>
        where T : IStage
        
    {
        Task ClearSite(Guid facilityId, Guid manifestId);
        Task SyncStage(List<T> extracts, Guid manifestId);
    }
}
