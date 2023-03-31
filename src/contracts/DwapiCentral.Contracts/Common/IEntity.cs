using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Common
{
    public interface IEntity
    {
      
        Guid Id { get; set; }
        int PatientPk { get; set; }
        int SiteCode { get; set; }
        string Emr { get; set; }
        string Project { get; set; }        
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
        bool Voided { get; set; }
       

        DateTime? Created { get; set; }
        DateTime? Updated { get; set; }
        DateTime? Extracted { get; set; }

        


    }
}
