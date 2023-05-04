using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Manifest
{
     public interface IManifest
    {
          Guid Id { get; set; }
          int SiteCode { get; set; }
          string Name { get; set; }
          Guid EmrId { get; set; }
          string EmrName { get; set; }            
          Guid Session { get; set; }
          DateTime Start { get; set; }
          DateTime? End { get; set; }
          string Tag { get; set; }
    }
}
