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
        public int patientPks { get; set; }
    }
}
