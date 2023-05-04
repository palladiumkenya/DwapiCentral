using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IAllergiesChronicIllnessExtract : IExtract, IAllergiesChronicIllness
    {
        Guid PatientId { get; set; }
    }
}
