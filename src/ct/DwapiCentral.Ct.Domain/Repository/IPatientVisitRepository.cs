using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{
    public interface IPatientVisitRepository 
    {
        Task AddAsync(PatientVisitExtract patientVisitExtract);
        Task UpdateAsync(PatientVisitExtract patientVisitExtract);
        Task MergeAsync(IEnumerable<PatientVisitExtract> patientVisitExtract);

    }
}