using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IPatientLaboratoryExtract : IExtract, ILab
    {
        Guid PatientId { get; set; }
    }
}