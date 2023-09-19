using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IDepressionScreeningProfile : IExtractProfile<DepressionScreeningExtract> { List<DepressionScreeningSourceDto> DepressionScreeningExtracts { get; set; } }
}