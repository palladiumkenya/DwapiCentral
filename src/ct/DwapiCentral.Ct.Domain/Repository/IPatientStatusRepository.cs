using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Domain.Repository
{

    public interface IPatientStatusRepository 
    {
        Task MergeAsync(IEnumerable<PatientStatusExtract> patientStatusExtracts);


    }
}