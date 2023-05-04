using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;
using System.Collections.Generic;


namespace DwapiCentral.Shared.Application.Interfaces.Ct.Profiles
{
    public interface IEnhancedAdherenceCounsellingProfile : IExtractProfile<EnhancedAdherenceCounsellingExtract> { List<EnhancedAdherenceCounsellingExtractDTO> EnhancedAdherenceCounsellingExtracts { get; set; } }
}