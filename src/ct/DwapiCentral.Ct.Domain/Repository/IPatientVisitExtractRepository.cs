using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Domain.Repository;

public interface IPatientVisitExtractRepository
{
    Task MergeAsync(IEnumerable<PatientVisitExtract> patientVisitExtracts);

    Task<PatientVisitExtract> GetByPatientDetails(int patientPk, int siteCode, int visitId, DateTime visitDate);
}