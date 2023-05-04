using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IAllergiesChronicIllnessExtractDTO : IExtractDTO, IAllergiesChronicIllness
    {
        Guid PatientId { get; set; }
    }
}