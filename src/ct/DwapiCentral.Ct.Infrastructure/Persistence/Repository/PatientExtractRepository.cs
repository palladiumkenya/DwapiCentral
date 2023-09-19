using Dapper;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;

using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using MediatR;
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
using System.Transactions;
using Z.Dapper.Plus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class PatientExtractRepository : IPatientExtractRepository
    {
        public readonly CtDbContext _context;

        public readonly IManifestRepository _manifestRepository;
       
        private readonly HashSet<string> patientHashes;

        private readonly IMediator _mediator;

        public PatientExtractRepository(CtDbContext context, IManifestRepository manifestRepository, IMediator mediator)
        {
            _context = context;
            
            this.patientHashes = new HashSet<string>();

            _manifestRepository = manifestRepository;

            _mediator = mediator;
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
            var batchPks = manifest.GetBatchPatientPKsJoined(5000);
            using (var connection = new SqlConnection(cons))
            {

                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {


                        var updateProcessedSql = @"
                                        UPDATE 
                                            PatientExtract 
                                        SET 
                                            Processed = 0  
                                        WHERE
                                            SiteCode = @siteCode";


                        await connection.ExecuteAsync(updateProcessedSql, new { siteCode = manifest.SiteCode }, transaction);

                        foreach (var batchPk in batchPks)
                        {

                            var updateBatchSql = @"
                                        UPDATE 
                                            PatientExtract 
                                        SET 
                                            Processed = 1  
                                        WHERE        
                                            SiteCode = @siteCode AND 
                                            PatientPk IN @BatchPk";


                            var parameters = new { siteCode = manifest.SiteCode, BatchPk = batchPk.Split(',').Select(int.Parse) };

                            await connection.ExecuteAsync(updateBatchSql, parameters, transaction);
                        }

                        var cleanUpSql = @"
                                        DELETE 
                                            FROM PatientExtract
                                        WHERE
                                            SiteCode = @siteCode AND
                                            Processed = 0";
                        
                        await connection.ExecuteAsync(cleanUpSql, new { siteCode = manifest.SiteCode }, transaction);

                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }
            }
            var manifestId = await _manifestRepository.GetManifestId(manifest.SiteCode);

            var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = manifest.PatientPKs.Count, ManifestId = manifestId, SiteCode = manifest.SiteCode, ExtractName = "PatientExtract", UploadMode = UploadMode.DifferentialLoad };
            await _mediator.Publish(notification);

        }
    }
}
