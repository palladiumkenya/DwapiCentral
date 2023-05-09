using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IDefaulterTracingExtract : IExtract, IDefaulterTracing
    {
        Guid PatientId { get; set; }
    }
}