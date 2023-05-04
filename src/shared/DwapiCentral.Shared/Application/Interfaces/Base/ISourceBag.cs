using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Base
{
    public interface ISourceBag<T>
    {
        string JobId { get; set; }
        EmrSetup EmrSetup { get; set; }
        UploadMode Mode { get; set; }
        string DwapiVersion { get; set; }
        int SiteCode { get; set; }
        string Facility { get; set; }
        Guid? ManifestId { get; set; }
        Guid? SessionId { get; set; }
        Guid? FacilityId { get; set; }
        string Tag { get; set; }
        int MinPk { get; set; }
        int MaxPk { get; set; }
        List<T> Extracts { get; set; }
    }
}
