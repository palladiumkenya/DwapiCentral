﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Manifest
{
    public interface IMetric
    {
        Guid Id { get; set; }       
        string Name { get; set; } // indicator, facility ,extract ,pks
        string Value { get; set; } // {}
        Guid ManifestId { get; set; }
    }
}
