using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public  interface IIptExtractDTO : IExtractDTO,IIpt
    {
        Guid PatientId { get; set; }
    }
}
