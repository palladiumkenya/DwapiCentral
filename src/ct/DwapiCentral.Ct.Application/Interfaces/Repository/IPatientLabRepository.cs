using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
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
