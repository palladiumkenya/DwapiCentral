using System.Collections.Generic;
using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Profiles
{
    public interface IContactListingProfile : IExtractProfile<ContactListingExtract> { List<ContactListingExtractDTO> ContactListingExtracts { get; set; } }
}