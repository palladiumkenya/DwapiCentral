using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientExtractRepository
    {
        Task MergeAsync(IEnumerable<PatientExtract> patientExtracts);
        Task processDifferentialPatients(FacilityManifest manifest);
    }
}
