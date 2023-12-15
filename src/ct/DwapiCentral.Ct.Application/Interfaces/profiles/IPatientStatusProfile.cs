using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System.Collections.Generic;

namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IPatientStatusProfile : IExtractProfile<PatientStatusExtract>
    {
        List<StatusSourceDto> StatusExtracts { get; set; }
    }
}