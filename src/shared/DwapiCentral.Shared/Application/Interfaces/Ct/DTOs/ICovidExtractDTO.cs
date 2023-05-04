using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface ICovidExtractDTO : IExtractDTO, ICovid
    {
        Guid PatientId { get; set; }
    }
}