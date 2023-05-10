using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Manifest
{
    public interface IMasterFacility
    {
         int Id { get; set; }
         int Code { get; set; }        
         string Name { get; set; }
         string County { get; set; }
         DateTime? SnapshotDate { get; set; }
         int? SnapshotSiteCode { get; set; }
         int? SnapshotVersion { get; set; }
    }
}
