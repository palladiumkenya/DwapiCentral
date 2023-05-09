using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{

    public interface IIptRepository : IRepository<IptExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<IptExtract> profileIptExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<IptExtract> extracts);

        void SyncNew(List<IptProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<IptProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
