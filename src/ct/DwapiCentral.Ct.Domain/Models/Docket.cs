using DwapiCentral.Ct.Domain.Custom;
using DwapiCentral.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Models
{
    public class Docket : Entity<string>
    {
        public string Name { get; set; }
        public string Instance { get; set; }
        public ICollection<Subscriber> Subscribers { get; set; } = new List<Subscriber>();

        public Docket()
        {
        }
        public bool SubscriberExists(string name)
        {
            return Subscribers.Any(x => x.Name.IsSameAs(name));
        }

        public bool SubscriberAuthorized(string name, string authcode)
        {
            return Subscribers.Any(x => x.Name.IsSameAs(name) && x.AuthCode.IsSameAs(authcode));
        }
    }
}
