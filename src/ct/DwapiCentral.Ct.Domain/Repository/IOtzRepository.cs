using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Domain.Repository
{

    public interface IOtzRepository 
    {

        Task<OtzExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID); Task UpdateExtract(List<OtzExtract> patientLabExtract); Task InsertExtract(List<OtzExtract> patientLabExtract);

    }
}
