using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IOvcProfile : IExtractProfile<OvcExtract> { List<OvcSourceDto> OvcExtracts { get; set; } }
}