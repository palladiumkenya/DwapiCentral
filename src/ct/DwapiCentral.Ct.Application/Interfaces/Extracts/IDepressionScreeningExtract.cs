using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IDepressionScreeningExtract : IExtract, IDepressionScreening
    {
        Guid PatientId { get; set; }
    }
}
