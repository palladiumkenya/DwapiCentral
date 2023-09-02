using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientBaseLinesRepository
    {
        Task MergeAsync(IEnumerable<PatientBaselinesExtract> patientBaselinesExtracts);
    }
}
