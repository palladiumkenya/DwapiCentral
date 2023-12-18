using AutoMapper;
using DwapiCentral.Prep.Domain.Models.Stage;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using log4net;
using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DwapiCentral.Prep.Domain.Repository.Stage;
using Microsoft.EntityFrameworkCore;
using Z.Dapper.Plus;
using DwapiCentral.Prep.Domain.Events;
using Dapper;

namespace DwapiCentral.Prep.Infrastructure.Persistence.Repository.Stage
{
    public class StagePrepCareTerminationRepository : IStagePrepCareTerminationRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly PrepDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StagePrepCareTerminationRepository(PrepDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StagePrepCareTermination)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StagePrepCareTermination> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var pks = extracts.Select(x => x.Id).ToList();

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);


                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PrepCareTerminationExtract" };
                await _mediator.Publish(notification);

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StagePrepCareTermination> stagePrepCt)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StagePrepCareTermination> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*
                            FROM PrepCareTerminations p 
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT DISTINCT PatientPK, SiteCode, PrepNumber,RecordUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, PrepNumber,RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.PrepNumber = s.PrepNumber  
                                    AND p.RecordUUID = s.RecordUUID
                                                                   
                            )
                        ";

                var existingRecords = await connection.QueryAsync<PrepCareTermination>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string PrepNumber, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.PrepNumber,x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stagePrepCt
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.PrepNumber,x.RecordUUID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stagePrepCt, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stagePrepCt;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StagePrepCareTermination> uniqueStageExtracts)
        {
            try
            {
                var latestRecordsDict = new Dictionary<string, StagePrepCareTermination>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.PrepNumber}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<PrepCareTermination>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StagePrepCareTermination> stageDrug, IEnumerable<PrepCareTermination> existingRecords)
        {
            try
            {
                //Update existing data
                var stageDictionary = stageDrug
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.PrepNumber,x.RecordUUID })
                         .ToDictionary(
                             g => g.Key,
                             g => g.FirstOrDefault()
                         );

                foreach (var existingExtract in existingRecords)
                {
                    if (stageDictionary.TryGetValue(
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.PrepNumber,existingExtract.RecordUUID },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }

                var cons = _context.Database.GetConnectionString();
                var sql = $@"
                           UPDATE 
                                     PrepCareTerminations

                               SET                                  
                                    DateExtracted = @DateExtracted,                                   
                                    FacilityName = @FacilityName,                                   
                                    HtsNumber = @HtsNumber,
                                    ExitDate = @ExitDate,
                                    ExitReason = @ExitReason,
                                    DateOfLastPrepDose = @DateOfLastPrepDose,
                                    Date_Created = @Date_Created,
                                    DateLastModified = @DateLastModified,
                                    Created = @Created,
                                    Updated = @Updated,
                                    Voided = @Voided  

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND PrepNumber = @PrepNumber
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

        private async Task UpdateLivestage(Guid manifestId, List<Guid> ids)
        {

            var cons = _context.Database.GetConnectionString();

            var sql = $@"
                            UPDATE 
                                    {_stageName}
                            SET 
                                    LiveStage= @nextlivestage 
                            
                            WHERE 
                                    ManifestId = @manifestId AND 
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
    }
}
