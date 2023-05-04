using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IGbvScreeningExtract : IExtract, IGbvScreening
    {
        Guid PatientId { get; set; }
    }
}
