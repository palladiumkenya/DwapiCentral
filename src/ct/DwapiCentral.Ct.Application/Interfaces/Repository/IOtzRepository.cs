using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{

    public interface IOtzRepository : IRepository<OtzExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<OtzExtract> profileOtzExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<OtzExtract> extracts);

        void SyncNew(List<OtzProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<OtzProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
