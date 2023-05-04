using DwapiCentral.Shared.Application.Interfaces.Ct.DTOs;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IPatientLaboratoryExtractDTO: IExtractDTO,ILaboratory
    {
        Guid PatientId { get; set; }
    }
}