using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IPatientVisitExtractDTO : IExtractDTO, IVisit
    {
        Guid PatientId { get; set; }
    }
}