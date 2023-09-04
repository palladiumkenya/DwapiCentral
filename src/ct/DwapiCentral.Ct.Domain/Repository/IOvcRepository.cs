using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IOvcRepository 
    {
        Task<OvcExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID); Task UpdateExtract(List<OvcExtract> patientLabExtract); Task InsertExtract(List<OvcExtract> patientLabExtract);
    }
}
