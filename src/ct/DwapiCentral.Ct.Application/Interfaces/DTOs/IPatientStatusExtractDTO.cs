using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IPatientStatusExtractDTO : IExtractDTO, IStatus
    {
        Guid PatientId { get; set; }
    }
}