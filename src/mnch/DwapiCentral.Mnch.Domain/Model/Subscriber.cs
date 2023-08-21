using DwapiCentral.Shared.Domain.Entities;
using System;


namespace DwapiCentral.Mnch.Domain.Model
{
    public class Subscriber : Entity<string>
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