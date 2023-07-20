using System.Data;
using System.Reflection;
using AutoMapper;
using Dapper;
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

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageStatusExtractRepository :IStageStatusExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageStatusExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageStatusExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageStatusExtract> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PatientStatusExtract" };
                await _mediator.Publish(notification);


                // assign > Assigned
                await AssignAll(manifestId, extracts.Select(x => x.Id).ToList());

                // Merge
                await MergeExtracts(manifestId, extracts);


            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageStatusExtract> stageStatus)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageStatusExtract> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    PatientPKs = stageStatus.Select(x => x.PatientPk),
                    SiteCodes = stageStatus.Select(x => x.SiteCode),
                    ExitDates = stageStatus.Select(x => x.ExitDate),
                    ExitReasons = stageStatus.Select(x => x.ExitReason)
                };

                var query = @"
                            SELECT p.*
                            FROM PatientStatusExtracts p
                            WHERE EXISTS (
                                SELECT 1
                                FROM StageStatusExtracts s
                                WHERE p.PatientPk = s.PatientPK
                                AND p.SiteCode = s.SiteCode 
                                AND P.ExitDate = s.ExitDate
                                AND P.ExitReason = s.ExitReason                               
                                
                            )
                        ";

                var existingRecords = await connection.QueryAsync<PatientStatusExtract>(query, queryParameters);
              
                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, DateTime ExitDate, string ExitReason)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.ExitDate, x.ExitReason)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageStatus
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.ExitDate, x.ExitReason)) && x.LiveSession == manifestId)
                        .ToList();
                      
                    //Update existing extracts
                    var stageDictionary = stageStatus.ToDictionary(
                        x => new { x.PatientPk, x.SiteCode, x.ExitDate, x.ExitReason },
                        x => x);

                    foreach (var existingExtract in existingRecords)
                    {
                        if (stageDictionary.TryGetValue(
                            new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.ExitDate, existingExtract.ExitReason },
                            out var stageExtract)
                        )
                        {
                            _mapper.Map(stageExtract, existingExtract);
                        }
                    }
                    _context.Database.GetDbConnection().BulkUpdate(existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageStatus;
                }

                var extracts = _mapper.Map<List<PatientStatusExtract>>(uniqueStageExtracts);
                _context.Database.GetDbConnection().BulkInsert(extracts);


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
    }
}
