

using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;

namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
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
