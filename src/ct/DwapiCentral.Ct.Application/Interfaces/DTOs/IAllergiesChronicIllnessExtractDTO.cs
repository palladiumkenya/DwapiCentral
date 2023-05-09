using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IAllergiesChronicIllnessExtractDTO : IExtractDTO, IAllergiesChronicIllness
    {
        Guid PatientId { get; set; }
    }
}