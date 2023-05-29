using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientExtractRepository
    {
        Task MergeAsync(IEnumerable<PatientExtract> patientExtracts);
    }
}
