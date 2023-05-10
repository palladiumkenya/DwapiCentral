using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace PalladiumDwh.Core.Interfaces
{

    public interface IEnhancedAdherenceCounsellingRepository : IRepository<EnhancedAdherenceCounsellingExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<EnhancedAdherenceCounsellingExtract> profileEnhancedAdherenceCounsellingExtracts);
      void ClearNew(Guid patientId);
      void SyncNew(Guid patientIdValue, IEnumerable<EnhancedAdherenceCounsellingExtract> extracts);


    }
}
