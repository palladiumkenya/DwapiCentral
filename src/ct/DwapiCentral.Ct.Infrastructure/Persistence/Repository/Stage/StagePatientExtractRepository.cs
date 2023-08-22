using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models.Extracts;
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

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extracts.Count,ManifestId=manifestId, SiteCode=extracts.First().SiteCode,ExtractName="PatientExtract" };
                await _mediator.Publish(notification);

                var pks = extracts.Select(x => new StagePatientExtract {PatientPk= x.PatientPk,SiteCode= x.SiteCode }).ToList();
               
                //update livestage var from rest to assigned
                await AssignAll(manifestId, pks);

                //create new patientrecords or update the existing patientRecords
                await Merge(manifestId, pks);

                
                await UpdateLivestage(manifestId, pks);
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

       

        private Task Merge(Guid manifestId, List<StagePatientExtract> stagePatients)
        {
            var connectionString = _context.Database.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var selectQuery = @"
                                SELECT 
                                       *,GETDATE() Created FROM StagePatientExtracts WITH (NOLOCK)
                                WHERE 
                                      LiveSession = @manifestId AND
                                      LiveStage = @livestage AND
                                      PatientPk = @patientPk AND
                                      SiteCode = @siteCode";

                foreach (StagePatientExtract stagePatient in stagePatients)
                {

                    // Step 1: Retrieve data from the stage table
                    List<StagePatientExtract> stageData = connection.Query<StagePatientExtract>(selectQuery,
                    new
                    {
                        manifestId,
                        livestage = LiveStage.Assigned,
                        patientPk = stagePatient.PatientPk,
                        siteCode = stagePatient.SiteCode
                    }).AsList();

                    // Step 2: Check if each record exists in the central table
                    List<PatientExtract> newRecords = new List<PatientExtract>();

                    foreach (StagePatientExtract stageRecord in stageData)
                    {
                        bool recordExists = CheckRecordExistence(connection, stageRecord);

                        if (recordExists)
                        {
                            // Update existing record in the central table
                            UpdateRecordInCentral(connection, stageRecord);
                        }
                        else
                        {
                            // Insert new record into the central table
                            InsertRecordIntoCentral(connection, stageRecord);
                        }
                    }
                }
                
                connection.Close();
            }

            

            return Task.CompletedTask;
        }


        private async Task UpdateLivestage(Guid manifestId, List<StagePatientExtract> pks)
        {

            var cons = _context.Database.GetConnectionString();

            var sql = @"
                            UPDATE 
                                    StagePatientExtracts
                            SET 
                                    LiveStage= @nextlivestage 
                            FROM 
                                    StagePatientExtracts 
                            WHERE 
                                    LiveSession = @manifestId AND 
                                    LiveStage= @livestage AND
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
                                    livestage = LiveStage.Assigned,
                                    nextlivestage = LiveStage.Merged,
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

        private bool CheckRecordExistence(SqlConnection connection, StagePatientExtract stageRecord)
        {
            string selectQuery = "SELECT COUNT(*) FROM PatientExtracts WHERE PatientPk = @PatientPk AND SiteCode = @SiteCode AND @RecordUUID = RecordUUID";

            int count = connection.ExecuteScalar<int>(selectQuery, stageRecord);
            return count > 0;
        }
        private void UpdateRecordInCentral(SqlConnection connection, StagePatientExtract stageRecord)
        {
            
            PatientExtract updateRecord = _mapper.Map<PatientExtract>(stageRecord);
            _context.Database.GetDbConnection().BulkMerge(updateRecord);
        }

        private void InsertRecordIntoCentral(SqlConnection connection, StagePatientExtract stageRecord)
        {
            PatientExtract newRecord = _mapper.Map<PatientExtract>(stageRecord);

            _context.Database.GetDbConnection().BulkInsert(newRecord);
        }

    }
}
