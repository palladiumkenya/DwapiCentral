using AutoMapper;
using Dapper;
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageIITRiskScoreRepository : IStageIITRiskScoreRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageIITRiskScoreRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"StageIITRiskScoresExtracts")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageIITRiskScore> extracts, Guid manifestId)
        {
            try
            {
                var pks = extracts.Select(x => x.Id).ToList();

                var result = await StageData(manifestId, pks);

                if (result == 0)
                {
                    // stage > Rest
                    _context.Database.GetDbConnection().BulkInsert(extracts);
                }
                // assign > Assigned
                await AssignAll(manifestId, pks);

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "IITRiskScoresExtract" };

                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageIITRiskScore> stageIItRiskScore)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageIITRiskScore> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Assigned
                };

                var query = $@"
                            SELECT p.*
                            FROM IITRiskScoresExtract p
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT DISTINCT PatientPK, SiteCode, SourceSysUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        LiveSession = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, SourceSysUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode                                    
                                    AND p.SourceSysUUID = s.SourceSysUUID
                                                                   
                            )
                        ";


                var existingRecords = await connection.QueryAsync<IITRiskScore>(query, queryParameters);

                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string SourceSysUUID, DateTime? Date_Created)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.SourceSysUUID, x.Date_Created)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates            
                    uniqueStageExtracts = stageIItRiskScore
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.SourceSysUUID,x.Date_Created)) && x.LiveSession == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageIItRiskScore, existingRecords,manifestId);


                }
                else
                {
                    uniqueStageExtracts = stageIItRiskScore;
                }

                await InsertNewDataFromStaging(uniqueStageExtracts,manifestId);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task AssignAll(Guid manifestId, List<Guid> ids)
        {
            var cons = _context.Database.GetConnectionString();

            var sql = $@"
                    UPDATE 
                            {_stageName}
                    SET 
                            LiveStage = @nextlivestage 
                    WHERE 
                            LiveSession = @manifestId AND 
                            LiveStage = @livestage AND 
                            Id IN @ids";
            try
            {
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                await connection.ExecuteAsync($"{sql}",
                    new
                    {
                        manifestId,
                        livestage = LiveStage.Rest,
                        nextlivestage = LiveStage.Assigned,
                        ids
                    }, null, 0);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }


        private async Task InsertNewDataFromStaging(List<StageIITRiskScore> uniqueStageExtracts,Guid manifestId)
        {
            try
            {
                var latestRecordsDict = new Dictionary<string, StageIITRiskScore>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.SourceSysUUID}";

                    if (!latestRecordsDict.ContainsKey(key) || extract.Date_Created > latestRecordsDict[key].Date_Created)
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<IITRiskScore>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                var notification = new OnErrorEvent { ExtractName = "IITRiskScoresExtract", ManifestId = manifestId, SiteCode = uniqueStageExtracts.First().SiteCode, message = ex.Message };
                await _mediator.Publish(notification);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageIITRiskScore> stageAdverse, IEnumerable<IITRiskScore> existingRecords,Guid manifestId)
        {
           
                try
                {
                    var centraldata = stageAdverse.Select(_mapper.Map<StageIITRiskScore, IITRiskScore>).ToList();


                    centraldata = centraldata.GroupBy(x => x.SourceSysUUID).Select(g => g.First()).ToList();

                    var existingIds = existingRecords.Select(x => x.SourceSysUUID).ToHashSet();

                    var recordsToUpdate = centraldata.Join(existingIds, x => x.SourceSysUUID, y => y, (x, y) => x).ToList();


                    const int maxRetries = 3;

                    for (var retry = 0; retry < maxRetries; retry++)
                    {
                        try
                        {
                        }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 1205)
                            {
                                _context.Database.GetDbConnection().BulkUpdate(recordsToUpdate);
                                await Task.Delay(100);
                            }
                            else
                            {
                                Log.Error(ex);
                                var notification = new OnErrorEvent { ExtractName = "IITRiskScoresExtract", ManifestId = manifestId, SiteCode = existingRecords.First().SiteCode, message = ex.Message };
                                await _mediator.Publish(notification);
                                throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    throw;
                }

                //{  var cons = _context.Database.GetConnectionString();
                //    using (var connection = new SqlConnection(cons))
                //    {
                //        await connection.OpenAsync();

                //        using (var transaction = connection.BeginTransaction())
                //        {
                //            const int maxRetries = 3;

                //            for (var retry = 0; retry < maxRetries; retry++)
                //            {
                //                try
                //                {
                //                    var sql = $@"
                //                   UPDATE 
                //                         ctr


                //                   SET
                //                        ctr.RiskScore = stg.RiskScore,
                //                        ctr.RiskFactors = stg.RiskFactors,
                //                        ctr.RiskDescription = stg.RiskDescription,
                //                        ctr.RiskEvaluationDate = stg.RiskEvaluationDate,
                //                        ctr.Date_Last_Modified = stg.Date_Last_Modified,                                   
                //                        ctr.DateLastModified = stg.DateLastModified,
                //                        ctr.DateExtracted = stg.DateExtracted,
                //                        ctr.Created = stg.Created,
                //                        ctr.Updated = stg.Updated,
                //                        ctr.Voided = stg.Voided   
                //                    FROM  IITRiskScoresExtract ctr
                //                    JOIN {_stageName} stg ON ctr.SourceSysUUID = stg.SourceSysUUID

                //                 WHERE  
                //                         ctr.SourceSysUUID = @SourceSysUUID";

                //                    await connection.ExecuteAsync(sql, existingRecords, transaction);
                //                    transaction.Commit();
                //                    break;
                //                }
                //                catch (SqlException ex)
                //                {
                //                    if (ex.Number == 1205)
                //                    {

                //                        await Task.Delay(100);
                //                    }
                //                    else
                //                    {
                //                        transaction.Rollback();
                //                        throw;
                //                    }
                //                }
                //            }
                //        }
                //    }
           
        }

    private async Task UpdateLivestage(Guid manifestId, List<Guid> ids)
        {

            var cons = _context.Database.GetConnectionString();

            var sql = $@"
                            UPDATE 
                                    {_stageName}
                            SET 
                                    LiveStage= @nextlivestage 
                            
                            WHERE 
                                    LiveSession = @manifestId AND 
                                    LiveStage= @livestage AND
                                    Id IN @ids";
            try
            {
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                await connection.ExecuteAsync($"{sql}",
                    new
                    {
                        manifestId,
                        livestage = LiveStage.Assigned,
                        nextlivestage = LiveStage.Merged,
                        ids
                    }, null, 0);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task<int> StageData(Guid manifestId, List<Guid> ids)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    ids

                };

                var query = $@"
                           
                                    SELECT 1
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        LiveSession = @manifestId 
                                         AND Id IN @ids                                   
                             
                        ";

                var result = await connection.QueryFirstOrDefaultAsync<int>(query, queryParameters);

                return result;

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
    }
}
