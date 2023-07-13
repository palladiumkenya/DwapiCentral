using DwapiCentral.Ct.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class IndicatorDto
    {
        public Guid Id { get; set; }
        public int FacilityCode { get; set; }
        public string FacilityName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Stage { get; set; }
        public DateTime? IndicatorDate { get; set; }
        public Guid FacilityManifestId { get; set; }

        public IndicatorDto(Guid id, int facilityCode, string facilityName, Guid facilityManifestId)
        {
            Id = id;
            FacilityCode = facilityCode;
            FacilityName = facilityName;
            FacilityManifestId = facilityManifestId;
            Stage = "EMR";
        }

        public static List<IndicatorDto> Generate(List<Metric> metrics)
        {
            var indicators = new List<IndicatorDto>();
            foreach (var m in metrics)
            {
                var idn = new IndicatorDto(m.Id, m.SiteCode, m.FacilityName, m.ManifestId);
                var cargo = JsonConvert.DeserializeObject<IndicatorItemDto>(m.Value);
                idn.Name = cargo.Indicator;
                idn.Value = cargo.IndicatorValue;
                idn.IndicatorDate = cargo.IndicatorDate;
                indicators.Add(idn);
            }
            return indicators;
        }
    }

    public class IndicatorItemDto
    {
        public string Indicator { get; set; }
        public string IndicatorValue { get; set; }
        public DateTime? IndicatorDate { get; set; }
    }
}
