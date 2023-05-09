using System;

namespace DwapiCentral.Ct.Application.Interfaces.Repository.Base
{
    public interface IClearPatientRecords
    {
        void Clear(Guid patientId);
    }
}