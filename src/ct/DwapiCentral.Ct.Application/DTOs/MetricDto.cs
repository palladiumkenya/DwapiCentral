using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class MetricDto
    {
        public Guid Id { get; set; }
        public int FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string Cargo { get; set; }
        public CargoType CargoType { get; set; }
        public Guid FacilityManifestId { get; set; }

        public MetricDto(Manifest manifest,  Metric manifestCargo)
        {
            Id = manifestCargo.Id;
            FacilityCode = manifest.SiteCode;
            FacilityName = manifest.Name;
            Cargo = manifestCargo.Value;
            CargoType = manifestCargo.Type;
            FacilityManifestId = manifestCargo.ManifestId;
        }

        public static List<MetricDto> Generate(Manifest facManifest)
        {
            var metrics = new List<MetricDto>();
            foreach (var cargo in facManifest.Metrics)
            {
                metrics.Add(new MetricDto(facManifest, cargo));
            }

            return metrics
                .Where(x => x.CargoType != CargoType.Patient)
                .ToList();
        }

    }
}
