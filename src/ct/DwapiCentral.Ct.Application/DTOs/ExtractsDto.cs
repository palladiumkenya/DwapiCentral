using DwapiCentral.Shared.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
        public class ExtractDto
        {
            public string Name { get; set; }
            public int? NoLoaded { get; set; }
            public string Version { get; set; }
            public string LogValue { get; set; }
            public DateTime? ActionDate { get; set; }
            public List<ExtractCargoDto> ExtractCargos { get; set; } = new List<ExtractCargoDto>();

            public static ExtractDto Generate(List<MetricDto> metricDtos)
            {
                var extractDto = new ExtractDto();

                var cargoes = metricDtos.Where(x =>
                        x.CargoType == CargoType.AppMetrics &&
                        x.Cargo.Contains("CareTreatment") &&
                        x.Cargo.Contains("ExtractCargos"))
                    .Select(c => c.Cargo)
                    .ToList();

                foreach (var cargo in cargoes)
                {
                    var temp = JsonConvert.DeserializeObject<ExtractDto>(cargo);
                    if (null != temp && !string.IsNullOrWhiteSpace(temp.LogValue))
                    {
                        extractDto = JsonConvert.DeserializeObject<ExtractDto>(temp.LogValue);
                        if (extractDto.ExtractCargos.Any())
                            return extractDto;
                    }
                }

                return extractDto;
            }

            public static List<ExtractCargoDto> GenerateCargo(List<MetricDto> metricDtos)
            {
                var extractDto = Generate(metricDtos);
                return extractDto.ExtractCargos;
            }
        
    }

    public class ExtractCargoDto
    {
        public string DocketId { get; set; }
        public string Name { get; set; }
        public int? Stats { get; set; }
    }
}
