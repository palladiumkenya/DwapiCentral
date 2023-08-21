using DwapiCentral.Mnch.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Application.Events
{
    public class MnchMetricsEvent : INotification
    {
        public List<MetricDto> HtsMetricExtracts { get; set; }
    }
}
