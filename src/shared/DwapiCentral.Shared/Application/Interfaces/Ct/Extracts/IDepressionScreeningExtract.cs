using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IDepressionScreeningExtract : IExtract, IDepressionScreening
    {
        Guid PatientId { get; set; }
    }
}
