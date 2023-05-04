using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct.Extracts
{
    public interface IEnhancedAdherenceCounsellingExtract : IExtract, IEnhancedAdherenceCounselling
    {
        Guid PatientId { get; set; }
    }
}
