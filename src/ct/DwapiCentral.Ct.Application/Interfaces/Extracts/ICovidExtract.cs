using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface ICovidExtract : IExtract, ICovid
    {
        Guid PatientId { get; set; }
    }
}