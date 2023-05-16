using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Ct.Domain.Custom;
using DwapiCentral.Shared.Domain.Entities;
using DwapiCentral.Shared.Domain.Entities.Ct;
using DwapiCentral.Shared.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class Facility : IFacility
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
    }
}
