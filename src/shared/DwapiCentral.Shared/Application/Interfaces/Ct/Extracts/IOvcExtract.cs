using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IOvcExtract : IExtract, IOvc
    {
        Guid PatientId { get; set; }
    }
}
