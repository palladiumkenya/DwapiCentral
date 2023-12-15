using DwapiCentral.Contracts.Common;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Ct
{
    public interface IStage 
    {
        Guid? FacilityId { get; set; }
        Guid? CurrentPatientId { get; set; }
        Guid? LiveSession { get; set; }
        LiveStage LiveStage { get; set; }
        DateTime? Generated { get; set; }
    }
}
