using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
{

    public interface IPatientStatusRepository : IRepository<PatientStatusExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientStatusExtract> profilePatientStatusExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientStatusExtract> extracts);

        void SyncNew(IEnumerable<PatientStatusProfile> profiles);

        void SyncNewPatients(IEnumerable<PatientStatusProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds);
    }
}