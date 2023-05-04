using System.Collections.Generic;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;


namespace DwapiCentral.Shared.Application.Interfaces.Ct.Profiles
{
    public interface IAllergiesChronicIllnessProfile : IExtractProfile<AllergiesChronicIllnessExtract>
    { 
        List<AllergiesChronicIllnessExtractDTO> AllergiesChronicIllnessExtracts { get; set; }
    }
}
