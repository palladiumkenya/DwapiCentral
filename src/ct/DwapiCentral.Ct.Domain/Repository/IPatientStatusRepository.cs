using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Domain.Repository
{

    public interface IPatientStatusRepository 
    {
        Task<PatientStatusExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID); Task UpdateExtract(List<PatientStatusExtract> patientLabExtract); Task InsertExtract(List<PatientStatusExtract> patientLabExtract);


    }
}