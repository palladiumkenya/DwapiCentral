using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IPatientPharmacyExtract : IExtract, IPharmacy
    {
        Guid PatientId { get; set; }
    }
}