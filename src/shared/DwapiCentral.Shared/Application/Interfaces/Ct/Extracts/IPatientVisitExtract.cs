using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IPatientVisitExtract : IExtract, IVisit
    {
        Guid PatientId { get; set; }
    }
}