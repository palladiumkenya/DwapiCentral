using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IPatientStatusExtract : IExtract, IStatus
    {
        Guid PatientId { get; set; }
    }
}