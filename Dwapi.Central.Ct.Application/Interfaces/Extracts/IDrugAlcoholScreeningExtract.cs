using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IDrugAlcoholScreeningExtract : IExtract, IDrugAlcoholScreening
    {
        Guid PatientId { get; set; }
    }
}
