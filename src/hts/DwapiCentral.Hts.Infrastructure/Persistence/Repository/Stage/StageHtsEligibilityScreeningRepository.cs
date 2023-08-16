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
    public class StageHtsEligibilityScreeningRepository : IStageHtsEligibilityScreeningRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly HtsDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageHtsEligibilityScreeningRepository(HtsDbContext context, IMapper mapper, IMediator mediator, string stageName = $"StageHtsEligibilityExtract")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageHtsEligibilityScreening> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "HtsEligibilityExtract" };
                await _mediator.Publish(notification);

                var pks = extracts.Select(x => x.Id).ToList();

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageHtsEligibilityScreening> stageEligibilityScreening)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageHtsEligibilityScreening> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*
                            FROM HtsEligibilityExtract p 
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT PatientPK, SiteCode, HtsNumber, EncounterId, VisitID,VisitDate
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, HtsNumber,EncounterId, VisitID,VisitDate
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.HtsNumber = s.HtsNumber  
                                    AND p.EncounterId = s.EncounterId
                                    AND p.VisitID = s.VisitID
                                    AND p.VisitDate = s.VisitDate
                            )
                        ";

                var existingRecords = await connection.QueryAsync<HtsEligibilityScreening>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string HtsNumber, string EncounterId, int? VisitID,  DateTime? VisitDate)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.HtsNumber, x.EncounterId,x.VisitID,x.VisitDate)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageEligibilityScreening
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.HtsNumber,x.EncounterId, x.VisitID, x.VisitDate)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageEligibilityScreening, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageEligibilityScreening;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageHtsEligibilityScreening> uniqueStageExtracts)
        {
            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageHtsEligibilityScreening>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.HtsNumber}_{extract.EncounterId}_{extract.VisitID}_{extract.VisitDate}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<HtsEligibilityScreening>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageHtsEligibilityScreening> stageDrug, IEnumerable<HtsEligibilityScreening> existingRecords)
        {
            try
            {
                //Update existing data
                var stageDictionary = stageDrug
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.HtsNumber, x.EncounterId,x.VisitID, x.VisitDate })
                         .ToDictionary(
                             g => g.Key,
                             g => g.FirstOrDefault()
                         );

                foreach (var existingExtract in existingRecords)
                {
                    if (stageDictionary.TryGetValue(
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.HtsNumber,existingExtract.EncounterId, existingExtract.VisitID, existingExtract.VisitDate },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }

                _context.Database.GetDbConnection().BulkUpdate(existingRecords);
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
