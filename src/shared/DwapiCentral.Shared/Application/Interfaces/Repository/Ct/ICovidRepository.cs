using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
{

    public interface ICovidRepository : IRepository<CovidExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<CovidExtract> profileCovidExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<CovidExtract> extracts);

        void SyncNew(List<CovidProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<CovidProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
