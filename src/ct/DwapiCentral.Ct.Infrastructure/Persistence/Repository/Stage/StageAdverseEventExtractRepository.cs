using System.Data;
using System.Reflection;
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
using Serilog;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageAdverseEventExtractRepository : IStageAdverseEventExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageAdverseEventExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageAdverseEventExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageAdverseEventExtract> extracts, Guid manifestId)
        {
            try
            {
                var pks = extracts.Select(x => x.Id).ToList();

                var result = await StageData(manifestId, pks);

                if (result == 0)
                {
                    
                    _context.Database.GetDbConnection().BulkInsert(extracts);
                }

                // assign > Assigned
                await AssignAll(manifestId,pks);

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);


                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PatientAdverseEventExtract" };
                await _mediator.Publish(notification);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageAdverseEventExtract> stageAdverse)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageAdverseEventExtract> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Assigned
                };

                var query = $@"
                            SELECT p.*
                            FROM PatientAdverseEventExtract p
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT DISTINCT PatientPK, SiteCode, RecordUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        LiveSession = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode                                    
                                    AND p.RecordUUID = s.RecordUUID
                                                                  
                            )
                        ";

              
                var existingRecords = await connection.QueryAsync<PatientAdverseEventExtract>(query, queryParameters);

                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {
                   
                    uniqueStageExtracts = stageAdverse
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID)) && x.LiveSession == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageAdverse, existingRecords,manifestId);


                }
                else
                {
                    uniqueStageExtracts = stageAdverse;
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


        private async Task InsertNewDataFromStaging(List<StageAdverseEventExtract> uniqueStageExtracts,Guid manifestId )
        {
            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageAdverseEventExtract>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<PatientAdverseEventExtract>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                var notification = new OnErrorEvent { ExtractName = "PatientAdverseEventExtract", ManifestId = manifestId, SiteCode = uniqueStageExtracts.First().SiteCode, message = ex.Message };
                await _mediator.Publish(notification);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageAdverseEventExtract> stageAdverse, IEnumerable<PatientAdverseEventExtract> existingRecords, Guid manifestId)
        {

                try
                {
                    var centraldata = stageAdverse.Select(_mapper.Map<StageAdverseEventExtract, PatientAdverseEventExtract>).ToList();

                    centraldata = centraldata.GroupBy(x => x.RecordUUID).Select(g => g.First()).ToList();

                    var existingIds = existingRecords.Select(x => x.RecordUUID).ToHashSet();

                    var recordsToUpdate = centraldata.Join(existingIds, x => x.RecordUUID, y => y, (x, y) => x).ToList();


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
                                var notification = new OnErrorEvent { ExtractName = "PatientAdverseEventExtract", ManifestId = manifestId, SiteCode = existingRecords.First().SiteCode, message = ex.Message };
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
                //try { 
                //    var cons = _context.Database.GetConnectionString();
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
                //               UPDATE 
                //                         pae


                //                   SET     
                //                           pae.VisitDate = sae.VisitDate
                //                          ,pae.AdverseEvent = sae.AdverseEvent
                //                          ,pae.AdverseEventStartDate = sae.AdverseEventStartDate
                //                          ,pae.AdverseEventEndDate = sae.AdverseEventEndDate
                //                          ,pae.Severity = sae.Severity
                //                          ,pae.AdverseEventClinicalOutcome = sae.AdverseEventClinicalOutcome
                //                          ,pae.AdverseEventActionTaken = sae.AdverseEventActionTaken
                //                          ,pae.AdverseEventIsPregnant = sae.AdverseEventIsPregnant
                //                          ,pae.AdverseEventRegimen = sae.AdverseEventRegimen
                //                          ,pae.AdverseEventCause = sae.AdverseEventCause
                //                          ,pae.Date_Created = sae.Date_Created
                //                          ,pae.DateLastModified = sae.DateLastModified
                //                          ,pae.DateExtracted = sae.DateExtracted
                //                          ,pae.Created = sae.Created
                //                          ,pae.Updated = sae.Updated
                //                          ,pae.Voided = sae.Voided   

                //                    FROM PatientAdverseEventExtract pae 
                //                    JOIN {_stageName} sae ON pae.RecordUUID = sae.RecordUUID

                //                 WHERE  pve.RecordUUID = @RecordUUID";

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
         

            //try
            //{
            //    var cons = _context.Database.GetConnectionString();
            //    //Update existing data
            //    var stageDictionary = stageAdverse
            //             .GroupBy(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
            //             .ToDictionary(
            //                 g => g.Key,
            //                 g => g.OrderByDescending(x => x.Date_Created).FirstOrDefault()
            //             );

            //    var updateTasks = existingRecords.Select(async existingExtract =>
            //    {
            //        if (stageDictionary.TryGetValue(
            //            new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.RecordUUID },
            //            out var stageExtract)
            //        )
            //        {
            //            _mapper.Map(stageExtract, existingExtract);
            //        }
            //    }).ToList();

            //    await Task.WhenAll(updateTasks);

            //     _context.Database.GetDbConnection().BulkUpdate(existingRecords);

            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex);
            //    throw;
            //}
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
