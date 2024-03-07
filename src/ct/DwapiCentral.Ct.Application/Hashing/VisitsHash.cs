using DwapiCentral.Ct.Application.DTOs.Source;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Hashing
{

public class VisitsHash
    {
        public static ulong ComputeChecksumHash(string data)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
               
                return BitConverter.ToUInt64(hashBytes, 0);
            }
        }
    }   

}
