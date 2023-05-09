using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IPatientPharmacyExtract : IExtract, IPharmacy
    {
        Guid PatientId { get; set; }
    }
}