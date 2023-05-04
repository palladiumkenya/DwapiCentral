using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IDefaulterTracingExtract : IExtract, IDefaulterTracing
    {
        Guid PatientId { get; set; }
    }
}