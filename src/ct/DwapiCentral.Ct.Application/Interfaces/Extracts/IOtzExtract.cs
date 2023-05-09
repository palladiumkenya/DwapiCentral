using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IOtzExtract : IExtract, IOtz
    {
        Guid PatientId { get; set; }
    }
}
