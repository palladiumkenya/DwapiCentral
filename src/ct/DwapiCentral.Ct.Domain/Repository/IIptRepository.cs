using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IIptRepository
    {
        Task<IptExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID); Task UpdateExtract(List<IptExtract> patientLabExtract); Task InsertExtract(List<IptExtract> patientLabExtract);
    }
}
