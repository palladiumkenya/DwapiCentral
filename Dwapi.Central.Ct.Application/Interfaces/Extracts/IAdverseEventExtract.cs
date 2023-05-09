using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IAdverseEventExtract : IExtract, IPatientAdverse
    {
        Guid PatientId { get; set; }
    }
}