using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
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