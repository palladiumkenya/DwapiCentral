using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Application.Interfaces.Ct.Extracts;
using System;

namespace PalladiumDwh.Shared.Interfaces.Extracts
{
    public interface IAdverseEventExtract : IExtract, IAdverseEvent
    {
        Guid PatientId { get; set; }
    }
}