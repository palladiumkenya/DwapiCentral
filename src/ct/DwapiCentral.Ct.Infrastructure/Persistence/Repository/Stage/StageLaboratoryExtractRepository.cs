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

                var notification = new ExtractsReceivedEvent { TotalExtractsCount = extracts.Count, SiteCode = extracts.First().SiteCode, ExtractName = "PatientLaboratoryExtract" };
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


                // Query existing records from the central table
                var existingRecords = await connection.QueryAsync<PatientLaboratoryExtract>("SELECT * FROM PatientLaboratoryExtracts WHERE PatientPk IN @PatientPKs AND SiteCode IN @SiteCodes AND VisitId IN @VisitIds AND OrderedByDate IN @OrderedByDates AND TestResult IN @TestResults AND TestName IN @TestNames",
                    new
                    {
                        PatientPKs = stageLab.Select(x => x.PatientPk),
                        SiteCodes = stageLab.Select(x => x.SiteCode),
                        VisitIds = stageLab.Select(x => x.VisitId),
                        OrderedByDates = stageLab.Select(x => x.OrderedByDate),
                        TestResults = stageLab.Select(x => x.TestResult),
                        TestNames = stageLab.Select(x => x.TestName)


                    });

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, int VisitId, DateTime OrderedByDate, string TestResult, string TestName)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.VisitId, x.OrderedByDate,x.TestResult,x.TestName)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageLab
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.VisitId,x.OrderedByDate,x.TestResult,x.TestName)) && x.LiveSession == manifestId)
                        .ToList();

                    //Update existing data
                    foreach (var existingExtract in existingRecords)
                    {
                        var stageExtract = stageLab.FirstOrDefault(x =>
                            x.PatientPk == existingExtract.PatientPk &&
                            x.SiteCode == existingExtract.SiteCode &&
                            x.VisitId == existingExtract.VisitId &&
                            x.OrderedByDate == existingExtract.OrderedByDate &&
                            x.TestResult == existingExtract.TestResult &&
                            x.TestName == existingExtract.TestName);

                        if (stageExtract != null)
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
