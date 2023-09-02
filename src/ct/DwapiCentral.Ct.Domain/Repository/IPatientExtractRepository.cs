using DwapiCentral.Ct.Domain.Models;


namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientExtractRepository
    {
        Task MergeAsync(IEnumerable<PatientExtract> patientExtracts);
        Task processDifferentialPatients(FacilityManifest manifest);
    }
}
