using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;
using System.Collections.Generic;


namespace DwapiCentral.Shared.Application.Interfaces.Ct.Profiles
{
    public interface IPatientPharmacyProfile : IExtractProfile<PatientPharmacyExtract>
    {
        List<PatientPharmacyExtractDTO> PharmacyExtracts { get; set; }

    }
}