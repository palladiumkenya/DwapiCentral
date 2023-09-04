using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface ICervicalCancerScreeningRepository
    {
        Task<CervicalCancerScreeningExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID);
        Task UpdateExtract(List<CervicalCancerScreeningExtract> patientLabExtract);
        Task InsertExtract(List<CervicalCancerScreeningExtract> patientLabExtract);
    }
}
