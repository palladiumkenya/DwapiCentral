using DwapiCentral.Prep.Domain.Models.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Repository.Stage
{
    public interface IStagePrepMonthlyRefillRepository
    {
        Task SyncStage(List<StagePrepMonthlyRefill> extracts, Guid manifestId);
    }
}
