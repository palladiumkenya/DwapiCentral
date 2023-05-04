using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IPatientStatusExtractDTO: IExtractDTO,IStatus
    {
        Guid PatientId { get; set; }
    }
}