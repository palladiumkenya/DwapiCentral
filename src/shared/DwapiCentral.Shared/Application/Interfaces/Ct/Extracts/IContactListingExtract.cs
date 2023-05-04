using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IContactListingExtract : IExtract, IContactListing
    {
        Guid PatientId { get; set; }
    }
}
