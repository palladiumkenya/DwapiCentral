using System.Collections.Generic;
using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Application.Interfaces.Profiles
{
    public interface IContactListingProfile : IExtractProfile<ContactListingExtract> { List<ContactListingExtractDTO> ContactListingExtracts { get; set; } }
}