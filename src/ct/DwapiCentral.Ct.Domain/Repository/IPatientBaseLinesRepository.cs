using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientBaseLinesRepository
    {
        Task<PatientBaselinesExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID); Task UpdateExtract(List<PatientBaselinesExtract> patientLabExtract); Task InsertExtract(List<PatientBaselinesExtract> patientLabExtract);
    }
}
