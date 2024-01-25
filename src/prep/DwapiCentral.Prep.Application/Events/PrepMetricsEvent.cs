using DwapiCentral.Prep.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Application.Events
{
    public class PrepMetricsEvent : INotification
    {
        public List<MetricDto> PrepMetricExtracts { get; set; }
    }
}
