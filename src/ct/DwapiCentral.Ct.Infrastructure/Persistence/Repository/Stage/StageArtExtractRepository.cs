using System.Data;
using System.Reflection;
using AutoMapper;
using Dapper;
using DwapiCentral.Contracts.Common;
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
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

               
                var pks = extracts.Select(x => x.Id).ToList();

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
                            FROM PatientArtExtracts p
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT PatientPK, SiteCode, RecordUUID, MAX(Date_Created) AS MaxCreatedTime
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        LiveSession = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode                                    
                                    AND p.RecordUUID = s.RecordUUID
                                    AND p.Date_Created = s.MaxCreatedTime                                    
                            )
                        ";
               

                var existingRecords = await connection.QueryAsync<PatientArtExtract>(query, queryParameters);
                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {                  
                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageArt
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID)) && x.LiveSession == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageArt, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageArt;
                }

                await InsertNewDataFromStaging(uniqueStageExtracts);

            }
            catch(Exception ex)
            {
                Log.Error(ex);
                throw;
            }
            
        }

        private async Task InsertNewDataFromStaging(List<StageArtExtract> uniqueStageExtracts)
        {
            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageArtExtract>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}";

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
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageArtExtract> stageArt, IEnumerable<PatientArtExtract> existingRecords)
        {
            try
            {

                //Update existing data
                var stageDictionary = stageArt
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.RecordUUID })
                         .ToDictionary(
                             g => g.Key,
                             g => g.OrderByDescending(x => x.Date_Created).FirstOrDefault()
                         );

                foreach (var existingExtract in existingRecords)
                {
                    if (stageDictionary.TryGetValue(
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.RecordUUID },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }

                var cons = _context.Database.GetConnectionString();
                var sql = $@"
                           UPDATE 
                                     PatientArtExtracts

                               SET     
                                     LastARTDate  = @LastARTDate
                                    ,LastVisit  = @LastVisit
                                    ,DOB  = @DOB
                                    ,AgeEnrollment  = @AgeEnrollment
                                    ,AgeARTStart  = @AgeARTStart
                                    ,AgeLastVisit  = @AgeLastVisit
                                    ,RegistrationDate  = @RegistrationDate
                                    ,Gender  = @Gender
                                    ,PatientSource  = @PatientSource
                                    ,StartARTDate  = @StartARTDate
                                    ,PreviousARTStartDate  = @PreviousARTStartDate
                                    ,PreviousARTRegimen  = @PreviousARTRegimen
                                    ,StartARTAtThisFacility  = @StartARTAtThisFacility
                                    ,StartRegimen  = @StartRegimen
                                    ,StartRegimenLine  = @StartRegimenLine
                                    ,LastRegimen  = @LastRegimen
                                    ,LastRegimenLine  = @LastRegimenLine
                                    ,Duration  = @Duration
                                    ,ExpectedReturn  = @ExpectedReturn
                                    ,Provider  = @Provider
                                    ,ExitReason  = @ExitReason
                                    ,ExitDate  = @ExitDate
                                    ,PreviousARTUse  = @PreviousARTUse
                                    ,PreviousARTPurpose  = @PreviousARTPurpose
                                    ,DateLastUsed  = @DateLastUsed
                                    ,Date_Created = @Date_Created
                                    ,DateLastModified = @DateLastModified
                                    ,DateExtracted = @DateExtracted
                                    ,Created = @Created
                                    ,Updated = @Updated
                                    ,Voided = @Voided                          

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND RecordUUID = @RecordUUID";

                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                await connection.ExecuteAsync(sql, existingRecords);
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


    }
}
