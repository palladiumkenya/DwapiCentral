using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public abstract class SourceDto
    {
        
        public int SiteCode { get; set; }
        public int PatientPk { get; set; }
        

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}
