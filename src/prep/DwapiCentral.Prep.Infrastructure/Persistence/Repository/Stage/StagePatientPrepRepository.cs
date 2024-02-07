using AutoMapper;
using Dapper;
using DwapiCentral.Prep.Domain.Events;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Models.Stage;
using DwapiCentral.Prep.Domain.Repository.Stage;
using DwapiCentral.Prep.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using log4net;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DwapiCentral.Prep.Infrastructure.Persistence.Repository.Stage
{
    internal class StagePatientPrepRepository : IStagePatientPrepRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly PrepDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public StagePatientPrepRepository(PrepDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task SyncStage(List<StagePatientPrep> extracts, Guid manifestId)
        {

            try
            {
                //stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

              

                var pks = extracts.Select(x => new StagePatientPrep { PatientPk = x.PatientPk, SiteCode = x.SiteCode, PrepNumber=x.PrepNumber, RecordUUID = x.RecordUUID }).ToList();

                //create new records or update the existing patientRecords
                await Merge(manifestId, pks);


                await UpdateLivestage(manifestId, pks);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PatientPrepExtract" };
                await _mediator.Publish(notification);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private Task Merge(Guid manifestId, List<StagePatientPrep> stagePatientPrep)
        {
            var connectionString = _context.Database.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var selectQuery = @"
                                SELECT DISTINCT
                                       *,GETDATE() Created FROM StagePrepPatients WITH (NOLOCK)
                                WHERE 
                                      ManifestId = @manifestId AND
                                      LiveStage = @livestage AND
                                      PatientPk = @patientPk AND
                                      SiteCode = @siteCode AND
                                      PrepNumber = @prepNumber AND 
                                      RecordUUID = @RecordUUID";


                foreach (StagePatientPrep stagePatient in stagePatientPrep)
                {

                    // Step 1: Retrieve data from the stage table
                    List<StagePatientPrep> stageData = connection.Query<StagePatientPrep>(selectQuery,
                    new
                    {
                        manifestId,
                        livestage = LiveStage.Rest,
                        patientPk = stagePatient.PatientPk,
                        siteCode = stagePatient.SiteCode,
                        prepNumber = stagePatient.PrepNumber,
                        recordUUID = stagePatient.RecordUUID,
                    }).AsList();

                    // Step 2: Check if each record exists in the central table
                    List<PatientPrepExtract> newRecords = new List<PatientPrepExtract>();

                    foreach (StagePatientPrep stageRecord in stageData)
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


        private async Task UpdateLivestage(Guid manifestId, List<StagePatientPrep> pks)
        {
            var cons = _context.Database.GetConnectionString();

            var sql = $@"
                            UPDATE 
                                    StagePrepPatients
                            SET 
                                    LiveStage= @nextlivestage 
                           
                            WHERE 
                                    ManifestId = @manifestId AND 
                                    LiveStage = @livestage AND   
                                    RecordUUID IN @recordUUIDs";
                                    
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
                                    livestage = LiveStage.Rest,
                                    nextlivestage = LiveStage.Assigned,
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

        private bool CheckRecordExistence(SqlConnection connection, StagePatientPrep stageRecord)
        {
            string selectQuery = "SELECT COUNT(*) FROM PrepPatients WHERE PatientPk = @PatientPk AND SiteCode = @SiteCode AND PrepNumber = @PrepNumber AND RecordUUID = @RecordUUID ";

            int count = connection.ExecuteScalar<int>(selectQuery, stageRecord);
            return count > 0;
        }
        private void UpdateRecordInCentral(SqlConnection connection, StagePatientPrep stageRecord)
        {

            PatientPrepExtract updateRecord = _mapper.Map<PatientPrepExtract>(stageRecord);
            _context.Database.GetDbConnection().BulkMerge(updateRecord);
        }

        private void InsertRecordIntoCentral(SqlConnection connection, StagePatientPrep stageRecord)
        {
            PatientPrepExtract newRecord = _mapper.Map<PatientPrepExtract>(stageRecord);

            _context.Database.GetDbConnection().BulkInsert(newRecord);
        }
    }
}
