using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IPatientVisitExtract : IExtract, IVisit
    {
        Guid PatientId { get; set; }
    }
}