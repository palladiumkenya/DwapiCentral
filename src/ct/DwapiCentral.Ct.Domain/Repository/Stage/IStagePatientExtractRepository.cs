using DwapiCentral.Ct.Domain.Models.Stage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository.Stage
{
    public interface IStagePatientExtractRepository
    {
        Task ClearSite(Guid facilityId);
        Task ClearSite(Guid facilityId, Guid manifestId);
        Task SyncStage(List<StagePatientExtract> extracts, Guid manifestId);
    }
}
