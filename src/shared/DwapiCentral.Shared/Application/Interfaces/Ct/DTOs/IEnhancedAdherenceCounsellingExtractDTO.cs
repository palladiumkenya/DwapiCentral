using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.DTOs
{
    public interface IEnhancedAdherenceCounsellingExtractDTO : IExtractDTO, IEnhancedAdherenceCounselling
    {
        Guid PatientId { get; set; }
    }
}