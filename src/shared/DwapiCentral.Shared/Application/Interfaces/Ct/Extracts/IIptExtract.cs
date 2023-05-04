using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IIptExtract : IExtract, IIpt
    {
        Guid PatientId { get; set; }
    }
}
