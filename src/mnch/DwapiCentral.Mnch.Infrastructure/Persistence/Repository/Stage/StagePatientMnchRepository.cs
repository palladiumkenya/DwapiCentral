using AutoMapper;
using Dapper;
using DwapiCentral.Mnch.Domain.Events;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Model.Stage;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using DwapiCentral.Mnch.Infrastructure.Persistence.Context;
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

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Repository.Stage
{
    public class StagePatientMnchRepository : IStagePatientMnchRepository
    {
        
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MnchDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public StagePatientMnchRepository(MnchDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task SyncStage(List<StagePatientMnchExtract> extracts, Guid manifestId)
        {

            try
            {
                //stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var pks = extracts.Select(x => new StagePatientMnchExtract { PatientPk = x.PatientPk, SiteCode = x.SiteCode}).ToList();

                //create new records or update the existing patientRecords
                await Merge(manifestId, extracts);


                await UpdateLivestage(manifestId, pks);


                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PatientMnchExtract" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private Task Merge(Guid manifestId, List<StagePatientMnchExtract> stageMnchPatients)
        {
            var connectionString = _context.Database.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var selectQuery = @"
                                SELECT 
                                       *,GETDATE() Created FROM StageMnchPatients WITH (NOLOCK)
                                WHERE 
                                      ManifestId = @manifestId AND
                                      LiveStage = @livestage AND
                                      PatientPk = @patientPk AND
                                      SiteCode = @siteCode AND 
                                      RecordUUID = @recordUUID";
                                       

                foreach (StagePatientMnchExtract stagePatient in stageMnchPatients)
                {

                    // Step 1: Retrieve data from the stage table
                    List<StagePatientMnchExtract> stageData = connection.Query<StagePatientMnchExtract>(selectQuery,
                    new
                    {
                        manifestId,
                        livestage = LiveStage.Rest,
                        patientPk = stagePatient.PatientPk,
                        siteCode = stagePatient.SiteCode,
                        recordUUID = stagePatient.RecordUUID,

                    }).AsList();

                    // Step 2: Check if each record exists in the central table
                    List<PatientMnchExtract> newRecords = new List<PatientMnchExtract>();

                    foreach (StagePatientMnchExtract stageRecord in stageData)
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


        private async Task UpdateLivestage(Guid manifestId, List<StagePatientMnchExtract> pks)
        {

            var cons = _context.Database.GetConnectionString();

            var sql = @"
                            UPDATE 
                                    StageMnchPatients
                            SET 
                                    LiveStage= @nextlivestage 
                            FROM 
                                    StageMnchPatients 
                            WHERE 
                                    ManifestId = @manifestId AND 
                                    LiveStage= @livestage AND
                                    PatientPk = @patientPk AND 
                                    SiteCode = @siteCode AND
                                    RecordUUID = @recordUUID";
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
                                    siteCode = pk.SiteCode,
                                    recordUUID = pk.RecordUUID

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

        private bool CheckRecordExistence(SqlConnection connection, StagePatientMnchExtract stageRecord)
        {
            string selectQuery = "SELECT COUNT(*) FROM MnchPatients WHERE PatientPk = @PatientPk AND SiteCode = @SiteCode AND RecordUUID = @RecordUUID";

            int count = connection.ExecuteScalar<int>(selectQuery, stageRecord);
            return count > 0;
        }
        private void UpdateRecordInCentral(SqlConnection connection, StagePatientMnchExtract stageRecord)
        {

            PatientMnchExtract updateRecord = _mapper.Map<PatientMnchExtract>(stageRecord);
            _context.Database.GetDbConnection().BulkMerge(updateRecord);
        }

        private void InsertRecordIntoCentral(SqlConnection connection, StagePatientMnchExtract stageRecord)
        {
            PatientMnchExtract newRecord = _mapper.Map<PatientMnchExtract>(stageRecord);

            _context.Database.GetDbConnection().BulkInsert(newRecord);
        }
    }
}
