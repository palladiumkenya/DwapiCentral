using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{

    public interface IDrugAlcoholScreeningRepository : IRepository<DrugAlcoholScreeningExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<DrugAlcoholScreeningExtract> profileDrugAlcoholScreeningExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<DrugAlcoholScreeningExtract> extracts);

    }
}
