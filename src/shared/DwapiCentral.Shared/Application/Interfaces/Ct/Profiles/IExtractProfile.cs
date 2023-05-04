using System;
using System.Collections.Generic;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Profiles
{
    public interface IExtractProfile<T> : IProfile
    {
        //Guid ProfileId { get; }
        List<T> Extracts { get; set; }
        bool IsValid();
        bool HasData();
        void GenerateRecords(Guid patientId);
    }
}
