using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IContactListingExtractDTO : IExtractDTO, IContactListing
    {
        Guid PatientId { get; set; }
    }
}