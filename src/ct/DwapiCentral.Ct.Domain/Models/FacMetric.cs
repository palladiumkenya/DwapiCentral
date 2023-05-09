using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Models
{
    public class FacMetric
    {
        public CargoType CargoType { get; set; }
        public string Metric { get; set; }
        public FacMetric()
        {

        }

        public FacMetric(CargoType cargoType, string metric)
        {
            CargoType = cargoType;
            Metric = metric;
        }
    }
}
