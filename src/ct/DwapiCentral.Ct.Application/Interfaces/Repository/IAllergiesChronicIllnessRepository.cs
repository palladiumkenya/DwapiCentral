using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models;
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

    }
}
