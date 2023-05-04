using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct
{
    public interface IClearPatientRecords
    {
        void Clear(Guid patientId);
    }
}