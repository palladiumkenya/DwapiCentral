using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IOtzExtractDTO : IExtractDTO, IOtz
    {
        Guid PatientId { get; set; }
    }
}