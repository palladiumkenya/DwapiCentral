using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using DwapiCentral.Contracts.Common;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using log4net;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StagePatientExtractRepository : IStagePatientExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public StagePatientExtractRepository(CtDbContext context,IMapper mapper,IMediator mediator)
        {
            _context = context;
            _mapper= mapper;
            _mediator= mediator;
        }



        public async Task ClearSite(int  siteCode)
        {

            var cons = _context.Database.GetDbConnection();

            var sql = @"

        delete  from StageAdverseEventExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageAllergiesChronicIllnessExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageArtExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageBaselineExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageContactListingExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageCovidExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageDefaulterTracingExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageDepressionScreeningExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageDrugAlcoholScreeningExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageEnhancedAdherenceCounsellingExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageGbvScreeningExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageIptExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageLaboratoryExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageOtzExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageOvcExtracts WHERE  SiteCode = @SiteCode;
        delete  from StagePatientExtracts WHERE  SiteCode = @SiteCode;
        delete  from StagePharmacyExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageStatusExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageVisitExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageCervicalCancerScreeningExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageIITRiskScoresExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageArtFastTrackExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageCancerScreeningExtracts WHERE  SiteCode = @SiteCode;
        delete  from StageRelationshipsExtracts WHERE  SiteCode = @SiteCode; 

        ";
            try
            {
               
                    if (cons.State != ConnectionState.Open)
                        cons.Open();

                    using (var transaction = cons.BeginTransaction())
                    {
                        await cons.ExecuteAsync($"{sql}", new { siteCode }, transaction, 0);
                        transaction.Commit();
                    }
               
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        public async Task ClearSite(Guid facilityId, Guid manifestId)
        {
            var cons = _context.Database.GetDbConnection();

            var sql = @"
                            DELETE FROM StagePatientExtracts 
                            WHERE 
                                    FacilityId = @facilityId AND
                                    LiveSession != @manifestId";
            try
            {
              
                    if (cons.State != ConnectionState.Open)
                        cons.Open();

                    using (var transaction = cons.BeginTransaction())
                    {
                        await cons.ExecuteAsync($"{sql}", new { manifestId = manifestId, facilityId }, transaction, 0);
                        transaction.Commit();
                    }
                
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        public async Task SyncStage(List<StagePatientExtract> extracts, Guid manifestId)
        {

            try
            {
                //stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var pks = extracts.Select(x => new StagePatientExtract {PatientPk= x.PatientPk,SiteCode= x.SiteCode,RecordUUID= x.RecordUUID }).ToList();
               
                //update livestage from rest to assigned
                await AssignAll(manifestId, pks);

                //create new patientrecords or update the existing patientRecords
                await Merge(manifestId, extracts);

                
                await UpdateLivestage(manifestId, pks);


                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PatientExtract" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task AssignAll(Guid manifestId, List<StagePatientExtract> pks)
        {
            var cons = _context.Database.GetConnectionString();

            var sql = @"
                            UPDATE 
                                    StagePatientExtracts
                            SET 
                                    LiveStage = @nextlivestage 
                            WHERE 
                                    LiveSession = @manifestId AND 
                                    LiveStage = @livestage AND 
                                    PatientPk = @patientPk AND 
                                    SiteCode = @siteCode";
            try
            {
                using (var connection = new SqlConnection(cons))
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    

                    using (var transaction = connection.BeginTransaction())
                    {
                        foreach (var pk in pks)
                        {
                            await connection.ExecuteAsync($"{sql}",
                            new
                            {
                                manifestId,
                                livestage = LiveStage.Rest,
                                nextlivestage = LiveStage.Assigned,
                                patientPk = pk.PatientPk,
                                siteCode = pk.SiteCode
                            }, transaction, 0);
                        }
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }



        //private async Task Merge(Guid manifestId, List<StagePatientExtract> stagePatients)
        //{
        //    var connectionString = _context.Database.GetConnectionString();

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        var selectQuery = @"
        //                        SELECT DISTINCT
        //                               *,GETDATE() Created FROM StagePatientExtracts WITH (NOLOCK)
        //                        WHERE 
        //                              LiveSession = @manifestId AND
        //                              LiveStage = @livestage AND
        //                              PatientPk = @patientPk AND
        //                              SiteCode = @siteCode";

        //        foreach (StagePatientExtract stagePatient in stagePatients)
        //        {

        //            // Step 1: Retrieve data from the stage table
        //            List<StagePatientExtract> stageData = connection.Query<StagePatientExtract>(selectQuery,
        //            new
        //            {
        //                manifestId,
        //                livestage = LiveStage.Assigned,
        //                patientPk = stagePatient.PatientPk,
        //                siteCode = stagePatient.SiteCode
        //            }).AsList();

        //            // Step 2: Check if each record exists in the central table
        //            List<PatientExtract> newRecords = new List<PatientExtract>();

        //            foreach (StagePatientExtract stageRecord in stageData)
        //            {
        //                bool recordExists = CheckRecordExistence(connection, stageRecord);

        //                if (recordExists)
        //                {
        //                    // Update existing record in the central table
        //                   await UpdateRecordInCentral(connection, stageRecord,manifestId);
        //                }
        //                else
        //                {
        //                    // Insert new record into the central table
        //                   await InsertRecordIntoCentral(connection, stageRecord,manifestId);
        //                }
        //            }
        //        }

        //        connection.Close();
        //    }




        //}

        public async Task Merge(Guid manifestId, List<StagePatientExtract> stagePatient)
        {
            try
            {
                int sitecode = stagePatient.First().SiteCode;

                var connectionString = _context.Database.GetConnectionString();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var query = $"SELECT * FROM PatientExtract WITH (NOLOCK) WHERE SiteCode = {sitecode}";

                    var existingRecords = await connection.QueryAsync<PatientExtract>(query);

                    var existingRecordDictionary = existingRecords.ToDictionary(record => new { record.PatientPk, record.SiteCode });

                    var recordsToInsert = new List<PatientExtract>();
                    var recordsToUpdate = new List<PatientExtract>();

                    foreach (var stageRecord in stagePatient)
                    {
                        var newRecord = _mapper.Map<PatientExtract>(stageRecord);

                        var key = new { newRecord.PatientPk, newRecord.SiteCode };

                        if (existingRecordDictionary.TryGetValue(key, out var existingRecord))
                        {
                            // Update existing record
                            _mapper.Map(stageRecord, existingRecord);
                            recordsToUpdate.Add(existingRecord);
                        }
                        else
                        {
                            // Insert new record
                            recordsToInsert.Add(newRecord);
                            existingRecordDictionary.Add(key, newRecord);
                        }
                    }


                    _context.Database.GetDbConnection().BulkInsert(recordsToInsert);


                    _context.Database.GetDbConnection().BulkUpdate(recordsToUpdate);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }



        private async Task UpdateLivestage(Guid manifestId, List<StagePatientExtract> pks)
        {

            var cons = _context.Database.GetConnectionString();

            var sql = $@"
                            UPDATE 
                                    StagePatientExtracts
                            SET 
                                    LiveStage= @nextlivestage 
                            
                            WHERE 
                                    LiveSession = @manifestId AND 
                                    LiveStage= @livestage AND
                                    RecordUUID IN @recordUUIDs "; 
            try
            {

                
                using (var connection = new SqlConnection(cons))
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        var recordUUIDs = pks.Select(pk => pk.RecordUUID).ToList();

                        await connection.ExecuteAsync($"{sql}",
                                new
                                {
                                    manifestId,
                                    livestage = LiveStage.Assigned,
                                    nextlivestage = LiveStage.Merged,
                                    recordUUIDs
                                }, transaction, 0);
                        
                        transaction.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private bool CheckRecordExistence(SqlConnection connection, StagePatientExtract stageRecord)
        {
            string selectQuery = "SELECT COUNT(*) FROM PatientExtract WHERE PatientPk = @PatientPk AND SiteCode = @SiteCode AND @RecordUUID = RecordUUID";

            int count = connection.ExecuteScalar<int>(selectQuery, stageRecord);
            return count > 0;
        }
        private async Task UpdateRecordInCentral(SqlConnection connection, StagePatientExtract stageRecord,Guid manifestId)
        {
            try
            {
                PatientExtract updateRecord = _mapper.Map<PatientExtract>(stageRecord);
                _context.Database.GetDbConnection().BulkMerge(updateRecord);
            }
            catch (Exception e)
            {
                Log.Error(e);
                var notification = new OnErrorEvent { ExtractName = "PatientExtract", ManifestId = manifestId, SiteCode = stageRecord.SiteCode, message = e.Message };
                await _mediator.Publish(notification);
                throw;
            }
        }

        private async Task InsertRecordIntoCentral(SqlConnection connection, StagePatientExtract stageRecord,Guid manifestId)
        {
            try
            {
                PatientExtract newRecord = _mapper.Map<PatientExtract>(stageRecord);

                _context.Database.GetDbConnection().BulkInsert(newRecord);
            }
            catch (Exception e)
            {
                Log.Error(e);
                var notification = new OnErrorEvent { ExtractName = "PatientExtract", ManifestId = manifestId, SiteCode = stageRecord.SiteCode, message = e.Message };
                await _mediator.Publish(notification);
                throw;

            }
        }

    }
}
