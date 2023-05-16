using DwapiCentral.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Contracts.Manifest;

namespace DwapiCentral.Ct.Domain.Models
{
    public class MasterFacility :IMasterFacility
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public string County { get; set; }
    }
}
