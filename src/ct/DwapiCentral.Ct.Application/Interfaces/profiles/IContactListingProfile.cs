using System.Collections.Generic;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IContactListingProfile : IExtractProfile<ContactListingExtract> { List<ContactListingSourceDto> ContactListingExtracts { get; set; } }
}