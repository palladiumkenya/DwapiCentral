using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IPatientAdverseEventExtractDTO : IExtractDTO, IAdverseEvent
    {
        Guid PatientId { get; set; }
    }
}