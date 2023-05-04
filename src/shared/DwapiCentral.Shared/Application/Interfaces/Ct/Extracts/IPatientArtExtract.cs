using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IPatientArtExtract : IExtract, IArt
    {
        Guid PatientId { get; set; }
    }
}