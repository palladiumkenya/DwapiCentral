using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Manifest
{
    public interface IMasterFacility
    {
         int Code { get; set; }     
         string Name { get; set; }
         string County { get; set; }
    }
}
