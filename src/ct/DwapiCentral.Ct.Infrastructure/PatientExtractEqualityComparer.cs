using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure
{
    public class PatientExtractEqualityComparer : IEqualityComparer<PatientExtract>
    {
        public bool Equals(PatientExtract? x, PatientExtract? y)
        {
            return x.PatientPk == y.PatientPk && x.SiteCode== y.SiteCode;
        }
      
        public int GetHashCode([DisallowNull] PatientExtract obj)
        {
            var hashString= $"{obj.PatientPk}{obj.SiteCode}";

            using(var sha = SHA256.Create())
            {
                var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(hashString));
                return BitConverter.ToInt32(hashBytes, 0);
            }
        }
    }
}
