using DwapiCentral.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Models
{
    public class Subscriber : Entity<Guid>
    {
        public string Name { get; set; }
        public string AuthCode { get; set; }
        public string DocketId { get; set; }

        public Subscriber()
        {
        }

        public Subscriber(string name, string authCode, string docketId)
        {
            Name = name;
            AuthCode = authCode;
            DocketId = docketId;
        }
    }
}
