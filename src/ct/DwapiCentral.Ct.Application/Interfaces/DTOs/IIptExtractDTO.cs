using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IIptExtractDTO : IExtractDTO, IIpt
    {
        Guid PatientId { get; set; }
    }
}
