using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Domain.Model.Common
{
    public class VerificationResponse
    {
        public string RegistryName { get; set; }
        public bool Verified { get; set; }

        public VerificationResponse(string registryName, bool verified)
        {
            RegistryName = registryName;
            Verified = verified;
        }
    }
}
