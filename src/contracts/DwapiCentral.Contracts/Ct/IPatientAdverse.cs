using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Ct
{
     public interface IPatientAdverse : IExtract
    {
          Guid Id { get; set; }
          DateTime? VisitDate { get; set; }
          string? AdverseEvent { get; set; }
          DateTime? AdverseEventStartDate { get; set; }
          DateTime? AdverseEventEndDate { get; set; }
          string? Severity { get; set; }
          string? AdverseEventClinicalOutcome { get; set; }
          string? AdverseEventActionTaken { get; set; }
          bool? AdverseEventIsPregnant { get; set; }          
          string? AdverseEventRegimen { get; set; }
          string? AdverseEventCause { get; set; }
         
    }
}
