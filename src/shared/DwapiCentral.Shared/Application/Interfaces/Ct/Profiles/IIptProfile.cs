using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;
using System.Collections.Generic;


namespace DwapiCentral.Shared.Application.Interfaces.Ct.Profiles
{
    public interface IIptProfile : IExtractProfile<IptExtract> { List<IptExtractDTO> IptExtracts { get; set; } }
}