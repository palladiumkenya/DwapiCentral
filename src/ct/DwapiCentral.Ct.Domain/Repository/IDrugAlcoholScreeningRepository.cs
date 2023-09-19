using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IDrugAlcoholScreeningRepository
    {
        Task<DrugAlcoholScreeningExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID); Task UpdateExtract(List<DrugAlcoholScreeningExtract> patientLabExtract); Task InsertExtract(List<DrugAlcoholScreeningExtract> patientLabExtract);
    }
}
