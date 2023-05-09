using System;
using System.Collections.Generic;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Repository
{
    public interface IPatientAdverseEventRepository : IRepository<PatientAdverseEventExtract>, IClearPatientRecords
    {
        void Sync(Guid patientIdValue, IEnumerable<PatientAdverseEventExtract> profilePatientStatusExtracts);
        void ClearNew(Guid patientId);
        void SyncNew(Guid patientIdValue, IEnumerable<PatientAdverseEventExtract> extracts);

    }
}