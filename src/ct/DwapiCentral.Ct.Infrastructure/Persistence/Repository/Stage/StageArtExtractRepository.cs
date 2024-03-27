using System.Data;
using System.Reflection;
using System.Transactions;
using AutoMapper;
using CSharpFunctionalExtensions;
using Dapper;
using DwapiCentral.Contracts.Common;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageArtExtractRepository : IStageArtExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageArtExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageArtExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

     
        public async Task SyncStage(List<StageArtExtract> extracts, Guid manifestId)
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

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PatientArtExtract" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageArtExtract> stageArt)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageArtExtract> uniqueStageExtracts;
                await connection.OpenAsync();
                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Assigned
                };

                var query = $@"
                            SELECT p.*
                            FROM PatientArtExtract p
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT DISTINCT Mhash, RecordUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        LiveSession = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY Mhash, RecordUUID
                                ) s
                                WHERE p.Mhash = s.Mhash                                                                     
                                    AND p.RecordUUID = s.RecordUUID
                                                                  
                            )
                        ";
               

                var existingRecords = await connection.QueryAsync<PatientArtExtract>(query, queryParameters);
                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(ulong Mhash, string RecordUUID)>(existingRecords.Select(x => (x.Mhash, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {                  
                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageArt
                        .Where(x => !existingRecordsSet.Contains((x.Mhash, x.RecordUUID)) && x.LiveSession == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageArt, existingRecords,manifestId);

                }
                else
                {
                    uniqueStageExtracts = stageArt;
                }

                await InsertNewDataFromStaging(uniqueStageExtracts,manifestId);

            }
            catch(Exception ex)
            {
                Log.Error(ex);
                throw;
            }
            
        }

        private async Task InsertNewDataFromStaging(List<StageArtExtract> uniqueStageExtracts, Guid manifestId)
        {
            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageArtExtract>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.Mhash}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<PatientArtExtract>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                var notification = new OnErrorEvent { ExtractName = "PatientArtExtract", ManifestId = manifestId, SiteCode = uniqueStageExtracts.First().SiteCode, message = ex.Message };
                await _mediator.Publish(notification);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageArtExtract> stageArt, IEnumerable<PatientArtExtract> existingRecords, Guid manifestId)
        {
           
                try
                {
                    var centraldata = stageArt.Select(_mapper.Map<StageArtExtract, PatientArtExtract>).ToList();

                    centraldata = centraldata.GroupBy(x => x.RecordUUID).Select(g => g.First()).ToList();

                    var existingIds = existingRecords.Select(x => x.RecordUUID).ToHashSet();

                    var recordsToUpdate = centraldata.Join(existingIds, x => x.RecordUUID, y => y, (x, y) => x).ToList();


                    const int maxRetries = 3;

                    for (var retry = 0; retry < maxRetries; retry++)
                    {
                        try
                        {
                        _context.Database.GetDbConnection().BulkUpdate(recordsToUpdate);
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
                                var notification = new OnErrorEvent { ExtractName = "PatientArtExtract", ManifestId = manifestId, SiteCode = existingRecords.First().SiteCode, message = ex.Message };
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
                //using (var connection = new SqlConnection(cons))
                //{
                //    await connection.OpenAsync();

                //    using (var transaction = connection.BeginTransaction())
                //    {
                //        const int maxRetries = 3;

                //        for (var retry = 0; retry < maxRetries; retry++)
                //        {
                //            try
                //            {

                //                var sql = $@"
                //               UPDATE ctr


                //               SET     
                //                     ctr.LastARTDate  = stg.LastARTDate
                //                    ,ctr.LastVisit  = stg.LastVisit
                //                    ,ctr.DOB  = stg.DOB
                //                    ,ctr.AgeEnrollment  = stg.AgeEnrollment
                //                    ,ctr.AgeARTStart  = stg.AgeARTStart
                //                    ,ctr.AgeLastVisit  = stg.AgeLastVisit
                //                    ,ctr.RegistrationDate  = stg.RegistrationDate
                //                    ,ctr.Gender  = stg.Gender
                //                    ,ctr.PatientSource  = stg.PatientSource
                //                    ,ctr.StartARTDate  = stg.StartARTDate
                //                    ,ctr.PreviousARTStartDate  = stg.PreviousARTStartDate
                //                    ,ctr.PreviousARTRegimen  = stg.PreviousARTRegimen
                //                    ,ctr.StartARTAtThisFacility  = stg.StartARTAtThisFacility
                //                    ,ctr.StartRegimen  = stg.StartRegimen
                //                    ,ctr.StartRegimenLine  = stg.StartRegimenLine
                //                    ,ctr.LastRegimen  = stg.LastRegimen
                //                    ,ctr.LastRegimenLine  = stg.LastRegimenLine
                //                    ,ctr.Duration  = stg.Duration
                //                    ,ctr.ExpectedReturn  = stg.ExpectedReturn
                //                    ,ctr.Provider  = stg.Provider
                //                    ,ctr.ExitReason  = stg.ExitReason
                //                    ,ctr.ExitDate  = stg.ExitDate
                //                    ,ctr.PreviousARTUse  = stg.PreviousARTUse
                //                    ,ctr.PreviousARTPurpose  = stg.PreviousARTPurpose
                //                    ,ctr.DateLastUsed  = stg.DateLastUsed
                //                    ,ctr.Date_Created = stg.Date_Created
                //                    ,ctr.DateLastModified = stg.DateLastModified
                //                    ,ctr.DateExtracted = stg.DateExtracted
                //                    ,ctr.Created = stg.Created
                //                    ,ctr.Updated = stg.Updated
                //                    ,ctr.Voided = stg.Voided
                //             FROM PatientArtExtract ctr
                //             JOIN {_stageName}  stg ON ctr.RecordUUID = stg.RecordUUID
                //             WHERE  ctr.RecordUUID = @RecordUUID";

                //                await connection.ExecuteAsync(sql, existingRecords, transaction);
                //                transaction.Commit();

                //                break;
                //            }
                //            catch (SqlException ex)
                //            {
                //                if (ex.Number == 1205)
                //                {

                //                    await Task.Delay(100);
                //                }
                //                else
                //                {
                //                    transaction.Rollback();
                //                    throw;
                //                }
                //            }
                //        }
                //    }
                //}
       

            //try
            //{

            //    //Update existing data
            //    var stageDictionary = stageArt
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

            //    _context.Database.GetDbConnection().BulkMerge(existingRecords);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex);
            //    throw;
            //}
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
            catch(Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }


    }
}
