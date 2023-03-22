using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
    public interface IHtsClientPartner : IEntity
    {
          string FacilityName { get; set; }
          int SiteCode { get; set; }
          int PatientPk { get; set; }
          string HtsNumber { get; set; }


          bool? Processed { get; set; }
          string QueueId { get; set; }
          string Status { get; set; }
          DateTime? StatusDate { get; set; }
          DateTime? DateExtracted { get; set; }
          String TracingType { get; set; }
          DateTime? TracingDate { get; set; }
          string TracingOutcome { get; set; }
          Guid FacilityId { get; set; }
    }
}
