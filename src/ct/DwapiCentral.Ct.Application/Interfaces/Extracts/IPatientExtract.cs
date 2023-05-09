using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IPatientExtract : IExtract, IPatient
    {
        int PatientPID { get; set; }
        string PatientCccNumber { get; set; }
        Guid FacilityId { get; set; }
    }
}
