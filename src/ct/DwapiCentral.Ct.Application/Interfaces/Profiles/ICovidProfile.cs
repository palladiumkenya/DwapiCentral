using System.Collections.Generic;
using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Profiles
{
    public interface ICovidProfile : IExtractProfile<CovidExtract>
    {
        List<CovidExtractDTO> CovidExtracts { get; set; }
    }
}
