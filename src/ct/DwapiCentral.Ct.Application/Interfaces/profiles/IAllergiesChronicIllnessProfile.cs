using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;

using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IAllergiesChronicIllnessProfile : IExtractProfile<AllergiesChronicIllnessExtract> { List<AllergiesChronicIllnessSourceDto> AllergiesChronicIllnessExtracts { get; set; } }
}
