using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace DwapiCentral.Shared.Application.Interfaces.Repository.Ct
{
    public interface IPatientLabRepository : IRepository<PatientLaboratoryExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientLaboratoryExtract> profilePatientLaboratoryExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientLaboratoryExtract> extracts);
        void SyncNew(IEnumerable<PatientLabProfile> profiles, IActionRegisterRepository repo);

        void SyncNewPatients(IEnumerable<PatientLabProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds, IActionRegisterRepository repo);
    }
}
