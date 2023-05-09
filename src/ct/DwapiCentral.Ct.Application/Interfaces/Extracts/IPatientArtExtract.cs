using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IPatientArtExtract : IExtract, IArt
    {
        Guid PatientId { get; set; }
    }
}