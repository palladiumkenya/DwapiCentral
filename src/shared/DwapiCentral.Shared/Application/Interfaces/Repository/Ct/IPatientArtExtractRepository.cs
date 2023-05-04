
using System;
using System.Collections.Generic;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Application.Interfaces.Repository.Ct;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.Profiles;


namespace PalladiumDwh.Core.Interfaces
{
    public interface IPatientArtExtractRepository : IRepository<PatientArtExtract>,IClearPatientRecords
    {
        void Sync(Guid patientId,IEnumerable<PatientArtExtract> extracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<PatientArtExtract> extracts);
        void SyncNew(IEnumerable<PatientARTProfile> profiles);

        void SyncNewPatients(IEnumerable<PatientARTProfile> profiles, IFacilityRepository facilityRepository,
            List<Guid> facIds);
    }
}
