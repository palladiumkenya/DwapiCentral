using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface ICervicalCancerScreeningProfile : IExtractProfile<CervicalCancerScreeningExtract> { List<CervicalCancerScreeningSourceDto> CervicalCancerScreeningExtracts { get; set; } }
}
