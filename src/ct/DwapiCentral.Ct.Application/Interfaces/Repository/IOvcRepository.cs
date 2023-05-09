using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{

    public interface IOvcRepository : IRepository<OvcExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<OvcExtract> profileOvcExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<OvcExtract> extracts);


    }
}
