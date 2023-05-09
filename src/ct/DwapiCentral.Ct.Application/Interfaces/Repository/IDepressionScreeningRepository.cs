using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository.Base
{

    public interface IDepressionScreeningRepository : IRepository<DepressionScreeningExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<DepressionScreeningExtract> profileDepressionScreeningExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<DepressionScreeningExtract> extracts);

        void SyncNew(List<DepressionScreeningProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<DepressionScreeningProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
