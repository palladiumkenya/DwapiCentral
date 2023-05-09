using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.DTOs
{
    public interface IEnhancedAdherenceCounsellingExtractDTO : IExtractDTO, IEnhancedAdherenceCounselling
    {
        Guid PatientId { get; set; }
    }
}