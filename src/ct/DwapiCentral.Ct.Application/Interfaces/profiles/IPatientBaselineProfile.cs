using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IPatientBaselineProfile : IExtractProfile<PatientBaselinesExtract>
    {
        List<PatientBaselineSourceDto> BaselinesExtracts { get; set; }
    }
}