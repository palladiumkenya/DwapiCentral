using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IIptExtract : IExtract, IIpt
    {
        Guid PatientId { get; set; }
    }
}
