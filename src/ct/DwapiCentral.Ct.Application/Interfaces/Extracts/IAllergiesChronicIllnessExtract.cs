using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Application.Interfaces.Extracts;
using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IAllergiesChronicIllnessExtract : IExtract, IAllergiesChronicIllness
    {
        Guid PatientId { get; set; }
    }
}