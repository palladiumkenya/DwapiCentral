using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Manifest
{
    public interface IFacility
    {
        int Code { get; set; }
        string Name { get; set; }
        DateTime? Created { get; set; }
    }
}
