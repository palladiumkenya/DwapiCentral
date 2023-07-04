using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Events
{
    public class PatientExtractsEvent : INotification
    {
        public int PatientPks { get; set; }

        public int SiteCode { get; set; }

        public string ExtractName { get; set; }
    }
}
