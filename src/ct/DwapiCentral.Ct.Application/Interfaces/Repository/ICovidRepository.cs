using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models.Extracts;


namespace DwapiCentral.Ct.Application.Interfaces.Repository.Base
{

    public interface ICovidRepository : IRepository<CovidExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<CovidExtract> profileCovidExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<CovidExtract> extracts);

        
    }
}
