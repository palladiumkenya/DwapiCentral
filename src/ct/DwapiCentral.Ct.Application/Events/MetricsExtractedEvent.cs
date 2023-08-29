using DwapiCentral.Ct.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Events
{
    public class MetricsExtractedEvent : INotification
    {
        public List<MetricDto> metricDtos { get; set; }
    }
}
