using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{

    public interface IOvcExtractDTO : IExtractDTO,IOvc
    {
        Guid PatientId { get; set; }
    }
}
