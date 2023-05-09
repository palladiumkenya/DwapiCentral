using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IPatientLaboratoryExtractDTO : IExtractDTO, ILab
    {
        Guid PatientId { get; set; }
    }
}