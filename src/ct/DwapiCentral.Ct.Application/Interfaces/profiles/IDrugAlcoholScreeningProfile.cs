using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IDrugAlcoholScreeningProfile : IExtractProfile<DrugAlcoholScreeningExtract> { List<DrugAlcoholScreeningSourceDto> DrugAlcoholScreeningExtracts { get; set; } }
}