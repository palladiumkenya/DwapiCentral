using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IDefaulterTracingExtractDTO : IExtractDTO, IDefaulterTracing
    {
        Guid PatientId { get; set; }
    }
}