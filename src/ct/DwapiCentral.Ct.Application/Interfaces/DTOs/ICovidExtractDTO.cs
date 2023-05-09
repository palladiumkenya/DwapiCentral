using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface ICovidExtractDTO : IExtractDTO, ICovid
    {
        Guid PatientId { get; set; }
    }
}