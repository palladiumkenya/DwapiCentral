using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Domain.Model.Common
{
    
    public class Subscriber
    {

        public string SubscriberId { get; set; }
        

        public string AuthToken { get; set; }

        public Subscriber()
        {
        }

        public Subscriber(string subscriberId, string authToken)
        {
            SubscriberId = subscriberId;
            AuthToken = authToken;
        }

        public bool Verify()
        {
            return CoreSubscribers().Any(
                x => x.SubscriberId.ToUpper() == SubscriberId &&
                     x.AuthToken.ToLower() == AuthToken.ToLower());
        }

        public List<Subscriber> CoreSubscribers()
        {
            return new List<Subscriber>
            {
                new Subscriber("DWAPI","1ba47c2a-6e05-11e8-adc0-fa7ae01bbebc"),
                new Subscriber("AMRS","6d7c7224-m26b-11a8-8un2-f2801f1b9fd1")
            };
        }
    }
}
