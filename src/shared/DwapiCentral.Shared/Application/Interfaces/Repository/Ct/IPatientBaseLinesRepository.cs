
using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Application.Interfaces.Repository.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientBaseLinesRepository : IRepository<PatientBaselinesExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientBaselinesExtract> patientArtExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientBaselinesExtract> extracts);
        void SyncNew(IEnumerable<PatientBaselineProfile> profiles);

        void SyncNewPatients(IEnumerable<PatientBaselineProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds);
    }
}
