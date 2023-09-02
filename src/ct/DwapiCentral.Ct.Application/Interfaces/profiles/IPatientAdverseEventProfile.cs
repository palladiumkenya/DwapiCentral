using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using System.Collections.Generic;


namespace DwapiCentral.Ct.Application.Interfaces.profiles
{
    public interface IPatientAdverseEventProfile : IExtractProfile<PatientAdverseEventExtract>
    {
        List<AdverseEventSourceDto> AdverseEventExtracts { get; set; }
    }
}