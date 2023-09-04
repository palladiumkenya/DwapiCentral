using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IDefaulterTracingRepository
    {
        Task<DefaulterTracingExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID); 
        Task UpdateExtract(List<DefaulterTracingExtract> patientLabExtract); 
        Task InsertExtract(List<DefaulterTracingExtract> patientLabExtract);
    }
}
