using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IDepressionScreeningExtractDTO : IExtractDTO, IDepressionScreening
    {
        Guid PatientId { get; set; }
    }
}