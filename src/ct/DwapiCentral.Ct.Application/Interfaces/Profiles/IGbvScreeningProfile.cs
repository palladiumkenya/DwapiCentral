using System.Collections.Generic;
using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Profiles
{
    public interface IGbvScreeningProfile : IExtractProfile<GbvScreeningExtract> { List<GbvScreeningExtractDTO> GbvScreeningExtracts { get; set; } }
}