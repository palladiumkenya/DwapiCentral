using AutoMapper;
using Dapper;
using DwapiCentral.Hts.Domain.Events;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Model.Stage;
using DwapiCentral.Hts.Domain.Repository.Stage;
using DwapiCentral.Hts.Infrastructure.Persistence.Context;
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

namespace DwapiCentral.Hts.Infrastructure.Persistence.Repository.Stage
{
    public class StageHtsClientRepository : IStageHtsClientRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HtsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public StageHtsClientRepository(HtsDbContext context, IMapper mapper, IMediator mediator)
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task SyncStage(List<StageHtsClient> extracts, Guid manifestId)
        {

            try
            {
                //stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "HtsClients" };
                await _mediator.Publish(notification);

                var pks = extracts.Select(x => new StageHtsClient { PatientPk = x.PatientPk, SiteCode = x.SiteCode, HtsNumber = x.HtsNumber }).ToList();

                //create new records or update the existing patientRecords
                await Merge(manifestId, pks);


                await UpdateLivestage(manifestId, pks);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private Task Merge(Guid manifestId, List<StageHtsClient> stageClients)
        {
            var connectionString = _context.Database.GetConnectionString();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var selectQuery = @"
                                SELECT 
                                       *,GETDATE() Created FROM StageClients WITH (NOLOCK)
                                WHERE 
                                      ManifestId = @manifestId AND
                                      LiveStage = @livestage AND
                                      PatientPk = @patientPk AND
                                      SiteCode = @siteCode   AND
                                      HtsNumber = @htsNumber ";

                foreach (StageHtsClient stageClient in stageClients)
                {

                    // Step 1: Retrieve data from the stage table
                    List<StageHtsClient> stageData = connection.Query<StageHtsClient>(selectQuery,
                    new
                    {
                        manifestId,
                        livestage = LiveStage.Rest,
                        patientPk = stageClient.PatientPk,
                        siteCode = stageClient.SiteCode,
                        htsNumber = stageClient.HtsNumber
                    }).AsList();

                    // Step 2: Check if each record exists in the central table
                    List<HtsClient> newRecords = new List<HtsClient>();

                    foreach (StageHtsClient stageRecord in stageData)
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


        private async Task UpdateLivestage(Guid manifestId, List<StageHtsClient> pks)
        {

            var cons = _context.Database.GetConnectionString();

            var sql = @"
                            UPDATE 
                                    StageClients
                            SET 
                                    LiveStage= @nextlivestage 
                            FROM 
                                    StageClients 
                            WHERE 
                                    ManifestId = @manifestId AND 
                                    LiveStage= @livestage AND
                                    PatientPk = @patientPk AND 
                                    SiteCode = @siteCode AND
                                    HtsNumber = @htsNumber ";
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
                                    htsNumber = pk.HtsNumber
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

        private bool CheckRecordExistence(SqlConnection connection, StageHtsClient stageRecord)
        {
            string selectQuery = "SELECT COUNT(*) FROM HtsClients WHERE PatientPk = @PatientPk AND SiteCode = @SiteCode AND HtsNumber = @HtsNumber";

            int count = connection.ExecuteScalar<int>(selectQuery, stageRecord);
            return count > 0;
        }
        private void UpdateRecordInCentral(SqlConnection connection, StageHtsClient stageRecord)
        {

            HtsClient updateRecord = _mapper.Map<HtsClient>(stageRecord);
            _context.Database.GetDbConnection().BulkMerge(updateRecord);
        }

        private void InsertRecordIntoCentral(SqlConnection connection, StageHtsClient stageRecord)
        {
            HtsClient newRecord = _mapper.Map<HtsClient>(stageRecord);

            _context.Database.GetDbConnection().BulkInsert(newRecord);
        }
    }
}
