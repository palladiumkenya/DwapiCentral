using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Application.DTOs.Extract;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class PatientExtractRepository : IPatientExtractRepository
    {
        public readonly CtDbContext _context;
        public readonly IDbConnection _connectionString;
        private readonly HashSet<string> patientHashes;

        public PatientExtractRepository(CtDbContext context, IDbConnection connectionString)
        {
            _context = context;
            _connectionString = connectionString;
            this.patientHashes= new HashSet<string>();
        }

        public async Task AddAsync(PatientExtract patientExtract)
        {
            string patientHash = GetPatientHash(patientExtract);
            if (patientHashes.Contains(patientHash))
            {
                throw new InvalidOperationException("Duplicate patient detected.");
            }

           // await _connectionString.BulkInsertAsync(patientExtract);
            

        }


        public async Task UpdateAsync(PatientExtract patientExtract)
        {
            _context.PatientExtracts.Update(patientExtract);
            await _context.SaveChangesAsync();
        }

        public async Task MergeAsync(IEnumerable<PatientExtract> patientExtract)
        {
            
        }

        private string GetPatientHash(PatientExtract patientExtract)
        {
            using (var shaAlgorithm = SHA256.Create())
            {
                var data = Encoding.UTF8.GetBytes($"{patientExtract.PatientPID}_{patientExtract.PatientPID}");
                var hashBytes = shaAlgorithm.ComputeHash(data);
                var hashStringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashStringBuilder.Append(hashBytes[i].ToString("X2"));
                }
                return hashStringBuilder.ToString();
            }


        }




    }
}
