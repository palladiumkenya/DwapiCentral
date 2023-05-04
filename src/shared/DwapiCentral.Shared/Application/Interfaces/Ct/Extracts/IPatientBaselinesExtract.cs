using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IPatientBaselinesExtract : IExtract, IBaseline
    {
        Guid PatientId { get; set; }
    }
}