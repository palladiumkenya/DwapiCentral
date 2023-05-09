using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
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
