using System.Collections.Generic;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface ICovidProfile : IExtractProfile<CovidExtract>
    {
        List<CovidSourceDto> CovidExtracts { get; set; }
    }
}
