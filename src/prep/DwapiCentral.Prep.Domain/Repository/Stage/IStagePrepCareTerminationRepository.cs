using DwapiCentral.Prep.Domain.Models.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Repository.Stage
{
    public interface IStagePrepCareTerminationRepository
    {
        Task SyncStage(List<StagePrepCareTermination> extracts, Guid manifestId);
    }
}
