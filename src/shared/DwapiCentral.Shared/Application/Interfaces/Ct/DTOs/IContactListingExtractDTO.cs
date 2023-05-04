using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IContactListingExtractDTO : IExtractDTO, IContactListing
    {
        Guid PatientId { get; set; }
    }
}