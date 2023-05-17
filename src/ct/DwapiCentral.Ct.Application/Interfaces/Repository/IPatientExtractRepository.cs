using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models.Extracts;
using Manifest = DwapiCentral.Ct.Domain.Models.Manifest;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{
    public interface IPatientExtractRepository : IRepository<PatientExtract>
    {
        PatientExtract Find(Guid facilityId, int patientPID);
        PatientExtract FindBy(Guid id);
        PatientExtract FindBy(Guid facilityId, int patientPID);
        Guid? GetPatientBy(Guid facilityId, string patientNumber);
        Guid? GetPatientBy(Guid facilityId, int patientPID);
        Guid? GetPatientByIds(Guid facilityId, int patientPID);
        Guid? Sync(PatientExtract patient);
        Guid? SyncNew(PatientExtract patient);
        
        Task ClearManifest(Manifest manifest);
        Task RemoveDuplicates(int siteCode);
        Task InitializeManifest(Manifest manifest);
    }
}
