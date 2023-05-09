using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IPatientAdverseEventExtractDTO : IExtractDTO, IPatientAdverse
    {
        Guid PatientId { get; set; }
    }
}