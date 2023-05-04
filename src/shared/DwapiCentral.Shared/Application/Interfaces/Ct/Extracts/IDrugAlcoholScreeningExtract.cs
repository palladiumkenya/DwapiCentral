using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IDrugAlcoholScreeningExtract : IExtract, IDrugAlcoholScreening
    {
        Guid PatientId { get; set; }
    }
}
