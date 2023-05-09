using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository.Base
{

    public interface IDefaulterTracingRepository : IRepository<DefaulterTracingExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<DefaulterTracingExtract> profileDefaulterTracingExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<DefaulterTracingExtract> extracts);

     
    }
}
