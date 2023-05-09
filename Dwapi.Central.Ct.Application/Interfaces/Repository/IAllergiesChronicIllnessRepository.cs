using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;


namespace DwapiCentral.Ct.Application.Interfaces.Repository
{
    public interface IAllergiesChronicIllnessRepository : IRepository<AllergiesChronicIllnessExtract>,
        IClearPatientRecords
    {
        void Sync(Guid patientIdValue,
            IEnumerable<AllergiesChronicIllnessExtract> profileAllergiesChronicIllnessExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<AllergiesChronicIllnessExtract> extracts);
        void SyncNew(List<AllergiesChronicIllnessProfile> profiles, IActionRegisterRepository repo);
        void SyncNewPatients(IEnumerable<AllergiesChronicIllnessProfile> profiles,
            IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
