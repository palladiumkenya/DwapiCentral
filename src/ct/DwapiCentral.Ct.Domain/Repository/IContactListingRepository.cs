using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IContactListingRepository
    {
        Task<ContactListingExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID);
        Task UpdateExtract(List<ContactListingExtract> patientLabExtract);
        Task InsertExtract(List<ContactListingExtract> patientLabExtract);
    }
}
