using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;



namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
{
    public interface IPatientVisitRepository : IRepository<PatientVisitExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientVisitExtract> profilePatientVisitExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientVisitExtract> extracts);
        void SyncNew(List<PatientVisitProfile> profiles, IActionRegisterRepository actionRegisterRepository);

        void SyncNewPatients(IEnumerable<PatientVisitProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository actionRegisterRepository);
    }
}