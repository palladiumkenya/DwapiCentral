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
    public class StageLaboratoryExtractRepository : IStageLaboratoryExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageLaboratoryExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageLaboratoryExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageLaboratoryExtract> extracts, Guid manifestId)
        {
            try
            {
                // stage 
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PatientLaboratoryExtract" };
                await _mediator.Publish(notification);


                // and manifestId, livestage Assigned
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

        private async Task MergeExtracts(Guid manifestId, List<StageLaboratoryExtract> stageLab)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageLaboratoryExtract> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    PatientPKs = stageLab.Select(x => x.PatientPk),
                    SiteCodes = stageLab.Select(x => x.SiteCode),
                    VisitIds = stageLab.Select(x => x.VisitId),
                    OrderedByDates = stageLab.Select(x => x.OrderedByDate),
                    TestResults = stageLab.Select(x => x.TestResult),
                    TestNames = stageLab.Select(x => x.TestName)

                };

                var query = @"
                            SELECT p.*
                            FROM PatientLaboratoryExtracts p
                            WHERE EXISTS (
                                SELECT 1
                                FROM StageLaboratoryExtracts s
                                WHERE p.PatientPk = s.PatientPK
                                AND p.SiteCode = s.SiteCode 
                                AND P.VisitId = s.VisitId
                                AND P.OrderedByDate = s.OrderedByDate 
                                AND P.TestResult = s.TestResult 
                                AND P.TestName = s.TestName 
                                
                            )
                        ";

                var existingRecords = await connection.QueryAsync<PatientLaboratoryExtract>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, int VisitId, DateTime OrderedByDate, string TestResult, string TestName)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.VisitId, x.OrderedByDate,x.TestResult,x.TestName)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageLab
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.VisitId,x.OrderedByDate,x.TestResult,x.TestName)) && x.LiveSession == manifestId)
                        .ToList();

                    //Update existing data                    
                    var stageDictionary = stageLab.ToDictionary(
                        x => new { x.PatientPk, x.SiteCode, x.VisitId, x.OrderedByDate,x.TestResult,x.TestName },
                        x => x);

                    foreach (var existingExtract in existingRecords)
                    {
                        if (stageDictionary.TryGetValue(
                            new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.VisitId, existingExtract.OrderedByDate,existingExtract.TestResult,existingExtract.TestName, },
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
                    uniqueStageExtracts = stageLab;
                }

                var labExtracts = _mapper.Map<List<PatientLaboratoryExtract>>(uniqueStageExtracts);
                _context.Database.GetDbConnection().BulkInsert(labExtracts);

            
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
