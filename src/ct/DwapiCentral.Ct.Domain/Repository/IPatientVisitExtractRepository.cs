using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Domain.Repository;

public interface IPatientVisitExtractRepository
{
    Task<PatientVisitExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID);
    Task UpdateExtract(List<PatientVisitExtract> patientLabExtract);
    Task InsertExtract(List<PatientVisitExtract> patientLabExtract);

}