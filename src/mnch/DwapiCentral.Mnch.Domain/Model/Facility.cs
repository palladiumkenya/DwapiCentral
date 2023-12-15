using DwapiCentral.Contracts.Manifest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Model
{
    public class Facility : IFacility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }

        public Facility(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
