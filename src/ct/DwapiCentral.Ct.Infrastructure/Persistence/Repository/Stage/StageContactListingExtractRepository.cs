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
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageContactListingExtractRepository :IStageContactListingExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageContactListingExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageContactListingExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageContactListingExtract> extracts, Guid manifestId)
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


                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "ContactListingExtract" };

                await _mediator.Publish(notification);


            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageContactListingExtract> stageContactListing)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageContactListingExtract> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Assigned
                };

                var query = $@"
                            SELECT p.*
                            FROM contactListingExtract p
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT DISTINCT PatientPK, SiteCode,RecordUUID
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


              var existingRecords = await connection.QueryAsync<ContactListingExtract>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode,string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode,x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageContactListing
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID)) && x.LiveSession == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageContactListing, existingRecords,manifestId);

                }
                else
                {
                    uniqueStageExtracts = stageContactListing;
                }

                await InsertNewDataFromStaging(uniqueStageExtracts,manifestId);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageContactListingExtract> uniqueStageExtracts,Guid manifestId)
        {
            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageContactListingExtract>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<ContactListingExtract>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                var notification = new OnErrorEvent { ExtractName = "ContactListingExtract", ManifestId = manifestId, SiteCode = uniqueStageExtracts.First().SiteCode, message = ex.Message };
                await _mediator.Publish(notification);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageContactListingExtract> stageContactListing, IEnumerable<ContactListingExtract> existingRecords,Guid manifestId)
        {

                try
                {
                    var centraldata = stageContactListing.Select(_mapper.Map<StageContactListingExtract, ContactListingExtract>).ToList();

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
                                var notification = new OnErrorEvent { ExtractName = "ContactListingExtract", ManifestId = manifestId, SiteCode = existingRecords.First().SiteCode, message = ex.Message };
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


                //var cons = _context.Database.GetConnectionString();
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
                //           UPDATE 
                //                     ContactListingExtract

                //               SET     

                //                    PartnerPersonID = @PartnerPersonID,
                //                    ContactAge = @ContactAge,
                //                    ContactSex = @ContactSex,
                //                    ContactMaritalStatus = @ContactMaritalStatus,
                //                    RelationshipWithPatient = @RelationshipWithPatient,
                //                    ScreenedForIpv = @ScreenedForIpv,
                //                    IpvScreening = @IpvScreening,
                //                    IPVScreeningOutcome = @IPVScreeningOutcome,
                //                    CurrentlyLivingWithIndexClient = @CurrentlyLivingWithIndexClient,
                //                    KnowledgeOfHivStatus = @KnowledgeOfHivStatus,
                //                    PnsApproach = @PnsApproach,
                //                    ContactPatientPK = @ContactPatientPK,
                //                    Date_Created = @Date_Created,
                //                    DateLastModified = @DateLastModified,
                //                    DateExtracted = @DateExtracted,
                //                    Created = @Created,
                //                    Updated = @Updated,
                //                    Voided = @Voided                          

                //             WHERE   RecordUUID = @RecordUUID";

                //    await connection.ExecuteAsync(sql, recordsToUpdate,transaction);
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
            //    var stageDictionary = stageContactListing
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

            //    _context.Database.GetDbConnection().BulkUpdate(existingRecords);
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
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }
    }
}
