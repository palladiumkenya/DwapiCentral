using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IOtzExtract : IExtract, IOtz
    {
        Guid PatientId { get; set; }
    }
}
