using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IPatientVisitProfile : IExtractProfile<PatientVisitExtract>
    {
        List<PatientVisitSourceDto> VisitExtracts { get; set; }
    }
}