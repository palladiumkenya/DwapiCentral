
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Application.DTOs
{
    public class MetricDto
    {
        public Guid Id { get; set; }
        public int FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string Cargo { get; set; }
        public CargoType CargoType { get; set; }
        public Guid FacilityManifestId { get; set; }

        public MetricDto(Manifest facilitymanifest, Cargo manifestCargo)
        {
            Id = manifestCargo.Id;
            FacilityCode = facilitymanifest.SiteCode;
            FacilityName = facilitymanifest.Name;
            Cargo = manifestCargo.Items;
            CargoType = manifestCargo.Type;
            FacilityManifestId = manifestCargo.ManifestId;
        }

        public MetricDto(int code, string fac, Cargo manifestCargo)
        {
            Id = manifestCargo.Id;
            FacilityCode = code;
            FacilityName = fac;
            Cargo = manifestCargo.Items;
            CargoType = manifestCargo.Type;
            FacilityManifestId = manifestCargo.ManifestId;
        }

        public static List<MetricDto> Generate(Manifest facManifest)
        {
            var metrics = new List<MetricDto>();
            foreach (var cargo in facManifest.Cargoes)
            {
                metrics.Add(new MetricDto(facManifest, cargo));
            }

            return metrics
                .Where(x => x.CargoType != CargoType.Patient)
                .ToList();
        }
    }
}
