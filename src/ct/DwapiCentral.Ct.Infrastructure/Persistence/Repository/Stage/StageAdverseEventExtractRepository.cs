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
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsCount = extracts.Count, SiteCode = extracts.First().SiteCode, ExtractName = "PatientAdverseEventExtract" };
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

        private async Task MergeExtracts(Guid manifestId, List<StageAdverseEventExtract> stageAdverse)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageAdverseEventExtract> uniqueStageExtracts;
                await connection.OpenAsync();


                // Query existing records from the central table
                var existingRecords = await connection.QueryAsync<PatientAdverseEventExtract>("SELECT * FROM PatientAdverseEventExtracts WHERE PatientPk IN @PatientPKs AND SiteCode IN @SiteCodes AND VisitDate IN @VisitDate",
                    new
                    {
                        PatientPKs = stageAdverse.Select(x => x.PatientPk),
                        SiteCodes = stageAdverse.Select(x => x.SiteCode),
                        VisitDate = stageAdverse.Select(x => x.VisitDate)

                    });

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, DateTime VisitDate)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.VisitDate)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates            
                    uniqueStageExtracts = stageAdverse
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.VisitDate)) && x.LiveSession == manifestId)
                        .ToList();

                    //Update existing data
                    foreach (var existingExtract in existingRecords)
                    {
                        var stageExtract = stageAdverse.FirstOrDefault(x =>
                            x.PatientPk == existingExtract.PatientPk &&
                            x.SiteCode == existingExtract.SiteCode &&
                            x.VisitDate == existingExtract.VisitDate);

                        if (stageExtract != null)
                        {
                            _mapper.Map(stageExtract, existingExtract);
                        }
                    }

                    _context.Database.GetDbConnection().BulkUpdate(existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageAdverse;
                }

                var adverseExtracts = _mapper.Map<List<PatientAdverseEventExtract>>(uniqueStageExtracts);
                _context.Database.GetDbConnection().BulkInsert(adverseExtracts);

                
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
