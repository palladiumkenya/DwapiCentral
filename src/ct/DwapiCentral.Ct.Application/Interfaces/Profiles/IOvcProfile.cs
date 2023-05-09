using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.Profiles
{
    public interface IOvcProfile : IExtractProfile<OvcExtract> { List<OvcExtractDTO> OvcExtracts { get; set; } }
}