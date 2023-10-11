using DwapiCentral.Mnch.Domain.Model.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Repository.Stage
{
    public interface IStagePatientMnchRepository
    {
        Task SyncStage(List<StagePatientMnchExtract> extracts, Guid manifestId);
    }
}
