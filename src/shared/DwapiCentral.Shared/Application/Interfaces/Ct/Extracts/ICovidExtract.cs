using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface ICovidExtract : IExtract, ICovid
    {
        Guid PatientId { get; set; }
    }
}