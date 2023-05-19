using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Domain.Repository;

public interface IPatientVisitExtractRepository
{
    Task MergeAsync(IEnumerable<PatientVisitExtract> patientVisitExtracts);
}