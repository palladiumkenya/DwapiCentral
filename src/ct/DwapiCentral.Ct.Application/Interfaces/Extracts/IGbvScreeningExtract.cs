using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IGbvScreeningExtract : IExtract, IGbvScreening
    {
        Guid PatientId { get; set; }
    }
}
