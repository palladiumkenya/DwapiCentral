using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

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
