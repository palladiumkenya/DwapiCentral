using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IPatientExtractDTO : IExtractDTO, IPatient
    {
        int PatientPID { get; set; }
        string PatientCccNumber { get; set; }
        Guid FacilityId { get; set; }
    }
}