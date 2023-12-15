using DwapiCentral.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Contracts.Manifest;

namespace DwapiCentral.Ct.Domain.Models
{
    public class MasterFacility :IMasterFacility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }
        public string Name { get; set; }
        public string County { get; set; }

        public MasterFacility(int code, string name, string county)
        {
            Code = code;
            Name = name;
            County = county;
        }
    }
}
