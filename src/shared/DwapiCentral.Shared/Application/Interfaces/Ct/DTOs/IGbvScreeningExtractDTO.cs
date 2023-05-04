using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public   interface IGbvScreeningExtractDTO : IExtractDTO,IGbvScreening
    {
        Guid PatientId { get; set; }
    }
}