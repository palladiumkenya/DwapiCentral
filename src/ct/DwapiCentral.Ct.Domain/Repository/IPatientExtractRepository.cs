using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientExtractRepository
    {
        Task AddAsync(PatientExtract patientExtract);
        Task UpdateAsync(PatientExtract patientExtract);
        Task MergeAsync(IEnumerable<PatientExtract> patientExtract);
    }
}
