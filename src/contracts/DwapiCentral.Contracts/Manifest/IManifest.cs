using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Manifest
{
     public interface IManifest
    {
          Guid Id { get; set; }
          string? Docket { get; set; }
          int SiteCode { get; set; }
          string Name { get; set; }
          string? Project { get; set; }
          string? DwapiVersion  { get; set; }
          Guid EmrId { get; set; }
          string EmrName { get; set; }            
          string? EmrVersion  { get; set; }
          Guid Session { get; set; }
          DateTime Start { get; set; }
          DateTime? End { get; set; }
          string Status { get; set; }
          DateTime StatusDate { get; set; }
          string? Tag { get; set; }
          DateTime Created { get; set; }


    }
}
