using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IPatientPharmacyExtractDTO: IExtractDTO,IPharmacy
    {
        Guid PatientId { get; set; }
    }
}