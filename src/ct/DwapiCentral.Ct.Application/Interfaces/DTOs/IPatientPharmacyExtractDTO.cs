using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IPatientPharmacyExtractDTO : IExtractDTO, IPharmacy
    {
        Guid PatientId { get; set; }
    }
}