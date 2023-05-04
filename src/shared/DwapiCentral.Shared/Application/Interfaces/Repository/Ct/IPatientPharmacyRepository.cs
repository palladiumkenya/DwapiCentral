
using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
{
    public interface IPatientPharmacyRepository : IRepository<PatientPharmacyExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientPharmacyExtract> profilePatientPharmacyExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientPharmacyExtract> extracts);

        void SyncNew(IEnumerable<PatientPharmacyProfile> profiles, IActionRegisterRepository actionRegisterRepository);

        void SyncNewPatients(IEnumerable<PatientPharmacyProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository actionRegisterRepository);
    }
}
