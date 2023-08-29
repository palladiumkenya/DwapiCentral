using Dapper;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class PatientExtractRepository : IPatientExtractRepository
    {
        public readonly CtDbContext _context;
       
        private readonly HashSet<string> patientHashes;

        public PatientExtractRepository(CtDbContext context)
        {
            _context = context;
            
            this.patientHashes = new HashSet<string>();
        }

        public Task MergeAsync(IEnumerable<PatientExtract> patientExtracts)
        {

            var existingPatientKeys = patientExtracts.Select(p => new { p.PatientPk, p.SiteCode }).ToList();

            // Query all patients from the database
            var allPatients = _context.PatientExtract.ToList();

            // Separate the new patients and existing patients that need to be updated
            var newPatients = new List<PatientExtract>();
            var patientsToUpdate = new List<PatientExtract>();

            foreach (var patient in patientExtracts)
            {
                var compositeKey = new { patient.PatientPk, patient.SiteCode };

                if (allPatients.Any(p => p.PatientPk == patient.PatientPk && p.SiteCode == patient.SiteCode))
                {
                    // Patient already exists, add to the update list
                    patientsToUpdate.Add(patient);
                }
                else
                {
                    // New patient, add to the insert list
                    newPatients.Add(patient);
                }
            }

            // Add new patients to the context           
            _context.Database.GetDbConnection().BulkMerge(newPatients);

            // Update existing patients in the context
            foreach (var patientToUpdate in patientsToUpdate)
            {
                var existingPatient = allPatients.FirstOrDefault(p =>
                    p.PatientPk == patientToUpdate.PatientPk && p.SiteCode == patientToUpdate.SiteCode);

                if (existingPatient != null)
                {
                    // Update the properties of the existing patient
                    existingPatient.Nupi = patientToUpdate.Nupi;
                    existingPatient.CccNumber = patientToUpdate.CccNumber;
                    
                }
            }

            // Save changes to the database
            _context.SaveChangesAsync();



            return Task.CompletedTask;
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
            _context.PatientExtract.Update(patientExtract);
            await _context.SaveChangesAsync();
        }


        private string GetPatientHash(PatientExtract patientExtract)
        {
            using (var shaAlgorithm = SHA256.Create())
            {
                var data = Encoding.UTF8.GetBytes($"{patientExtract.PatientPk}_{patientExtract.SiteCode}");
                var hashBytes = shaAlgorithm.ComputeHash(data);
                var hashStringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashStringBuilder.Append(hashBytes[i].ToString("X2"));
                }

                return hashStringBuilder.ToString();
            }
        }

        public async Task processDifferentialPatients(FacilityManifest manifest)
        {
            Log.Debug($"clearing {manifest.SiteCode}...");
            var cons = _context.Database.GetConnectionString();

            var updateProcessedSql = @"
                                        UPDATE 
                                            PatientExtract 
                                        SET 
                                            Processed = 0  
                                        WHERE
                                            SiteCode = @SiteCode";

            var batchUpdateSql = @"
                                        UPDATE 
                                            PatientExtract 
                                        SET 
                                            Processed = 1  
                                        WHERE        
                                            SiteCode = @SiteCode AND 
                                            PatientPID IN @BatchPks";

            var cleanUpSql = @"
                                        DELETE 
                                            FROM PatientExtract
                                        WHERE
                                            SiteCode = @SiteCode AND
                                            Processed = 0";

            var batchPks = manifest.GetBatchPatientPKsJoined(5000);

            using (var connection = new SqlConnection(cons))
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        await connection.ExecuteAsync(updateProcessedSql, new { SiteCode = manifest.SiteCode }, transaction);
                        await connection.ExecuteAsync(batchUpdateSql, new { SiteCode = manifest.SiteCode, BatchPks = batchPks }, transaction);
                        await connection.ExecuteAsync(cleanUpSql, new { SiteCode = manifest.SiteCode }, transaction);
                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
            }
        }    
    }
}
