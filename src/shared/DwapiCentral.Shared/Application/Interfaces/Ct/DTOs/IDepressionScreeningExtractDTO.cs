using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IDepressionScreeningExtractDTO : IExtractDTO, IDepressionScreening
    {
        Guid PatientId { get; set; }
    }
}