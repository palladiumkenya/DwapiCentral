using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Prep
{
     public interface IPrepCareTermination : IExtract
    {

          string PrepNumber { get; set; }
          string? FacilityName { get; set; }         
          string? HtsNumber { get; set; }
          DateTime? ExitDate { get; set; }
          string? ExitReason { get; set; }
          DateTime? DateOfLastPrepDose { get; set; }

    }
}
