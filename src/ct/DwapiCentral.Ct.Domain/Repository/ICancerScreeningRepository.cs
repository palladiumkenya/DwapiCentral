using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface ICancerScreeningRepository
    {
        Task<CancerScreeningExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID);
        Task UpdateExtract(List<CancerScreeningExtract> patientLabExtract);
        Task InsertExtract(List<CancerScreeningExtract> patientLabExtract);
    }
}
