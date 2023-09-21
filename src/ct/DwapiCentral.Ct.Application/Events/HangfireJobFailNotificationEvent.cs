using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Events
{
    public class HangfireJobFailNotificationEvent : INotification
    {
        public string Message { get; set; }
    }
}
