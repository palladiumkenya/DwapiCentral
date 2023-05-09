using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IPatientArtExtractDTO : IExtractDTO, IArt
    {
        Guid PatientId { get; set; }
    }
}