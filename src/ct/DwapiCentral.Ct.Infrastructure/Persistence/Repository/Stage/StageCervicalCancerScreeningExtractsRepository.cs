using AutoMapper;
using Dapper;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
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

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageCervicalCancerScreeningExtractsRepository : IStageCervicalCancerScreeningExtractsRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageCervicalCancerScreeningExtractsRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageCervicalCancerScreeningExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageCervicalCancerScreeningExtract> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "CervicalCancerScreeningExtract" };
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

        private async Task MergeExtracts(Guid manifestId, List<StageCervicalCancerScreeningExtract> stageCancerScreening)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageCervicalCancerScreeningExtract> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    PatientPKs = stageCancerScreening.Select(x => x.PatientPk),
                    SiteCodes = stageCancerScreening.Select(x => x.SiteCode),
                    VisitIds = stageCancerScreening.Select(x => x.VisitID),
                    VisitDates = stageCancerScreening.Select(x => x.VisitDate)
                };
                var query = @"
                            SELECT p.*
                            FROM CervicalCancerScreeningExtracts p
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT PatientPK, SiteCode, VisitID, VisitDate, MAX(Date_Created) AS MaxCreatedTime
                                    FROM StageCervicalCancerScreeningExtracts
                                    GROUP BY PatientPK, SiteCode, VisitID, VisitDate
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.VisitID = s.VisitID
                                    AND p.VisitDate = s.VisitDate
                                    AND p.Date_Created = s.MaxCreatedTime
                            )
                        ";

                var existingRecords = await connection.QueryAsync<CervicalCancerScreeningExtract>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, int? VisitID, DateTime? VisitDate)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.VisitID, x.VisitDate)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageCancerScreening
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.VisitID, x.VisitDate)) && x.LiveSession == manifestId)
                        .ToList();

                    //Update existing data
                    var stageDictionary = stageCancerScreening
                             .GroupBy(x => new { x.PatientPk, x.SiteCode, x.VisitID, x.VisitDate })
                             .ToDictionary(
                                 g => g.Key,
                                 g => g.OrderByDescending(x => x.Date_Created).FirstOrDefault()
                             );

                    foreach (var existingExtract in existingRecords)
                    {
                        if (stageDictionary.TryGetValue(
                            new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.VisitID, existingExtract.VisitDate },
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
                    uniqueStageExtracts = stageCancerScreening;
                }
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageCervicalCancerScreeningExtract>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.VisitID}_{extract.VisitDate}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<CervicalCancerScreeningExtract>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);

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
