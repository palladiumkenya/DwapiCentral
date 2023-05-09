using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IOvcExtract : IExtract, IOvc
    {
        Guid PatientId { get; set; }
    }
}
