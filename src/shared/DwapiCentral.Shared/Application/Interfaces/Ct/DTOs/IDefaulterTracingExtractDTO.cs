using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IDefaulterTracingExtractDTO : IExtractDTO, IDefaulterTracing
    {
        Guid PatientId { get; set; }
    }
}