using DwapiCentral.Contracts.Ct;
using System;

namespace DwapiCentral.Ct.Application.Interfaces.Extracts
{
    public interface IEnhancedAdherenceCounsellingExtract : IExtract, IEnhancedAdherenceCounselling
    {
        Guid PatientId { get; set; }
    }
}
