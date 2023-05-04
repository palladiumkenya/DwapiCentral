using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Application.Interfaces.Repository.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientAdverseEventRepository : IRepository<PatientAdverseEventExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientAdverseEventExtract> profilePatientStatusExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientAdverseEventExtract> extracts);

        void SyncNew(List<PatientAdverseEventProfile> profiles, IActionRegisterRepository actionRegisterRepository);

        void SyncNewPatients(IEnumerable<PatientAdverseEventProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository actionRegisterRepository);
    }
}