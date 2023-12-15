using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface IPatientLaboratoryExtractRepository
    {

        Task<PatientLaboratoryExtract> GetPatientLabExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID);
        Task UpdatePatientLabExtract(List<PatientLaboratoryExtract> patientLabExtract);
        Task InsertPatientLabExtract(List<PatientLaboratoryExtract> patientLabExtract);
    }
}
