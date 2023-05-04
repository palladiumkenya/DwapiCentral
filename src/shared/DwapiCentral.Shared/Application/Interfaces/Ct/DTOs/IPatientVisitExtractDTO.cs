using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IPatientVisitExtractDTO: IExtractDTO,IVisit
    {
        Guid PatientId { get; set; }
    }
}