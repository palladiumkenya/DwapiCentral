using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{

    public interface IPatientStatusRepository : IRepository<PatientStatusExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientStatusExtract> profilePatientStatusExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientStatusExtract> extracts);

     
    }
}