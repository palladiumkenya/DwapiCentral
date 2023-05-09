using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IPatientBaselinesExtract : IExtract, IPatientBaselines
    {
        Guid PatientId { get; set; }
    }
}