using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IGbvScreeningExtractDTO : IExtractDTO, IGbvScreening
    {
        Guid PatientId { get; set; }
    }
}