using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IEnhancedAdherenceCounsellingRepository
    {
        Task<EnhancedAdherenceCounsellingExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID); Task UpdateExtract(List<EnhancedAdherenceCounsellingExtract> patientLabExtract); Task InsertExtract(List<EnhancedAdherenceCounsellingExtract> patientLabExtract);
    }
}
