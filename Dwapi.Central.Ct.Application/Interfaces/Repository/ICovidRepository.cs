using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;


namespace DwapiCentral.Ct.Application.Interfaces.Repository.Base
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
