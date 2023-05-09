using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IPatientBaselinesExtractDTO : IExtractDTO, IPatientBaselines
    {
        Guid PatientId { get; set; }
    }
}