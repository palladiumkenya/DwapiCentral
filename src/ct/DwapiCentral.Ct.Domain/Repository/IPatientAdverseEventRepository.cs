using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientAdverseEventRepository
    {
        Task MergeAsync(IEnumerable<PatientAdverseEventExtract> patientAdverseEventExtracts);

    }
}