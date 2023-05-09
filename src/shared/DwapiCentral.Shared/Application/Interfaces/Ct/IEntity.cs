using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Ct
{
    public interface IEntity
        {
            Guid Id { get; set; }
            string Emr { get; set; }
            string Project { get; set; }
            bool Voided { get; set; }
            bool Processed { get; set; }
        }
    
}
