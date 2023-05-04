using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public    interface IOtzExtractDTO : IExtractDTO,IOtz
    {
        Guid PatientId { get; set; }
    }
}