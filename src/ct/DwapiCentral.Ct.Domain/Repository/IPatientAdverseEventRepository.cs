using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientAdverseEventRepository
    {
        Task<PatientAdverseEventExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID); Task UpdateExtract(List<PatientAdverseEventExtract> patientLabExtract); Task InsertExtract(List<PatientAdverseEventExtract> patientLabExtract);

    }
}