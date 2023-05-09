using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{
    public interface IPatientArtExtractRepository : IRepository<PatientArtExtract>, IClearPatientRecords
    {
        void Sync(Guid patientId, IEnumerable<PatientArtExtract> extracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientArtExtract> extracts);
        void SyncNew(IEnumerable<PatientARTProfile> profiles);

        void SyncNewPatients(IEnumerable<PatientARTProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds);
    }
}
