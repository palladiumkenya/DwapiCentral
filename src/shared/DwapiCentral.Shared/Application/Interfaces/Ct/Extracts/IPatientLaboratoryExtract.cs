using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IPatientLaboratoryExtract : IExtract, ILaboratory
    {
        Guid PatientId { get; set; }
    }
}