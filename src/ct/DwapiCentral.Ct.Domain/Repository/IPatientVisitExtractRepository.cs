using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Domain.Repository;

public interface IPatientVisitExtractRepository
{
    Task MergeAsync(IEnumerable<PatientVisitExtract> patientVisitExtracts);

    
}