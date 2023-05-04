using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IPatientStatusExtract : IExtract, IStatus
    {
        Guid PatientId { get; set; }
    }
}