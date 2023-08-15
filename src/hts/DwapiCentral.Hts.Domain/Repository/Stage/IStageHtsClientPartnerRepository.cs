using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Model.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Repository.Stage
{
    public interface IStageHtsClientPartnerRepository
    {
        Task SyncStage(List<StageHtsClientPartner> extracts, Guid manifestId);
    }
}
