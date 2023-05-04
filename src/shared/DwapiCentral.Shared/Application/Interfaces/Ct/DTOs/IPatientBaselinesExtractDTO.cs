using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IPatientBaselinesExtractDTO: IExtractDTO,IBaseline
    {
        Guid PatientId { get; set; }
    }
}