using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IIITRiskScoresProfile : IExtractProfile<IITRiskScore> { List<IITRiskScoreSourceDto> IITRiskScoresExtracts { get; set; } }
}
