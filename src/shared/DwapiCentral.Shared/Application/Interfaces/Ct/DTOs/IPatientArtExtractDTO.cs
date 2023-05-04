using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IPatientArtExtractDTO:IExtractDTO,IArt
    {
        Guid PatientId { get; set; }
    }
}