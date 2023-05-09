using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{

    public interface IOvcExtractDTO : IExtractDTO, IOvc
    {
        Guid PatientId { get; set; }
    }
}
