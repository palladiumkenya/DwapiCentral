using DwapiCentral.Hts.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Application.Events
{
    public class HtsMetricsEvent : INotification
    {
        public List<MetricDto> HtsMetricExtracts { get; set; }
    }
}
