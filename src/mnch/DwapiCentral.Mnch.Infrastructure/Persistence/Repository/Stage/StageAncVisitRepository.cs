using DwapiCentral.Mnch.Domain.Model.Stage;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Repository.Stage
{
    public class StageAncVisitRepository : IStageAncVisitRepository
    {
        public Task SyncStage(List<StageAncVisit> extracts, Guid manifestId)
        {
            throw new NotImplementedException();
        }
    }
}
