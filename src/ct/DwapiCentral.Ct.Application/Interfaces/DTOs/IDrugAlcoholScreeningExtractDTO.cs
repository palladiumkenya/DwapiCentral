using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IDrugAlcoholScreeningExtractDTO : IExtractDTO, IDrugAlcoholScreening
    {
        Guid PatientId { get; set; }
    }
}