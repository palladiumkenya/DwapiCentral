using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IDrugAlcoholScreeningExtractDTO : IExtractDTO, IDrugAlcoholScreening
    {
        Guid PatientId { get; set; }
    }
}