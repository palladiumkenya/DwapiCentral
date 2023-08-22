using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Models
{
    public class ManifestResponse
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public Guid ManifestId { get; set; }
        public Guid SessionId { get; set; }
        public string JobId { get; set; }
        public Guid FacilityId { get; set; }
        
        public static ManifestResponse Create(FacilityManifest manifest)
        {
            var fm = new ManifestResponse()
            {
                Code = manifest.SiteCode,
                Name = manifest.Name,
                ManifestId = manifest.Id,
                SessionId = manifest.Session
            };           
            return fm;
        }
    }
}
