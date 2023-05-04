using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;
using System.Collections.Generic;


namespace DwapiCentral.Shared.Application.Interfaces.Ct.Profiles
{
    public interface IPatientBaselineProfile : IExtractProfile<PatientBaselinesExtract>
    {
        List<PatientBaselinesExtractDTO> BaselinesExtracts { get; set; }
    }
}