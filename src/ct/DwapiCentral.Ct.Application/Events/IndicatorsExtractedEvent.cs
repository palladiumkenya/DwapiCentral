using DwapiCentral.Ct.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Events
{
    public class IndicatorsExtractedEvent
    {
        public List<IndicatorDto> IndicatorsExtracts { get; set; }
    }
}
