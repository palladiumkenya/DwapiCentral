using System.Data;
using System.Linq;
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
    public class StagePharmacyExtractRepository :IStagePharmacyExtractRepository
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StagePharmacyExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StagePharmacyExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StagePharmacyExtract> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsCount = extracts.Count, SiteCode = extracts.First().SiteCode, ExtractName = "PatientPharmacyExtract" };
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

        private async Task MergeExtracts(Guid manifestId, List<StagePharmacyExtract> stagePharmacy)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StagePharmacyExtract> uniqueStageExtracts;
                await connection.OpenAsync();


                var queryParameters = new
                {
                    stagePharmacyPatientPKs = stagePharmacy.Select(x => x.PatientPk),
                    stagePharmacySiteCodes = stagePharmacy.Select(x => x.SiteCode),
                    stagePharmacyDateExtracted = stagePharmacy.Select(x=> x.DateExtracted),
                    stagePharmacyDispenseDates = stagePharmacy.Select(x => x.DispenseDate)
                };

                var query = @"
                            SELECT p.*
                            FROM PatientPharmacyExtracts p
                            WHERE EXISTS (
                                SELECT 1
                                FROM StagePharmacyExtracts s
                                WHERE p.PatientPk = s.PatientPK
                                AND p.SiteCode = s.SiteCode
                                AND p.DateExtracted = s.DateExtracted
                                AND p.DispenseDate = s.DispenseDate
                            )
                        ";

                var existingRecords = await connection.QueryAsync<PatientPharmacyExtract>(query, queryParameters);


                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, DateTime? DateExtracted, DateTime DispenseDate)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode,x.DateExtracted, x.DispenseDate)));

                if (existingRecordsSet.Any())
                {
                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stagePharmacy
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode,x.DateExtracted, x.DispenseDate)) && x.LiveSession == manifestId)
                        .ToList();

                    //Update existing data                    
                    var stagePharmacyDictionary = stagePharmacy.ToDictionary(
                        x => new { x.PatientPk, x.SiteCode,x.DateExtracted, x.DispenseDate},
                        x => x );

                    
                    foreach (var existingExtract in existingRecords)
                    {
                        if (stagePharmacyDictionary.TryGetValue(
                            new { existingExtract.PatientPk, existingExtract.SiteCode,existingExtract.DateExtracted, existingExtract.DispenseDate, },
                            out var stageExtract)
                        )
                        {
                            _mapper.Map(stageExtract, existingExtract);
                        }
                    }

                    // Bulk update the existingRecords
                    _context.Database.GetDbConnection().BulkUpdate(existingRecords);                  

                }
                else
                {
                    uniqueStageExtracts = stagePharmacy;
                }

                var extracts = _mapper.Map<List<PatientPharmacyExtract>>(uniqueStageExtracts);
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
