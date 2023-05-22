using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientPharmacyRepository
    {
        Task MergePharmacyExtractsAsync(IEnumerable<PatientPharmacyExtract> pharmacyExtracts);
    }
}
