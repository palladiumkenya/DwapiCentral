using AutoMapper;
using DwapiCentral.Prep.Domain.Models.Stage;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Repository.Stage;
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
using Microsoft.EntityFrameworkCore;
using Dapper;
using Z.Dapper.Plus;
using DwapiCentral.Prep.Domain.Events;

namespace DwapiCentral.Prep.Infrastructure.Persistence.Repository.Stage
{
    public class StagePrepMonthlyRefillsRepository : IStagePrepMonthlyRefillRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly PrepDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StagePrepMonthlyRefillsRepository(PrepDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StagePrepMonthlyRefill)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StagePrepMonthlyRefill> extracts, Guid manifestId)
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

                

                // Merge
                await MergeExtracts(manifestId, extracts);

                await UpdateLivestage(manifestId, pks);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PrepMonthlyRefills" };
                await _mediator.Publish(notification);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StagePrepMonthlyRefill> stagePrepmonthlyRefill)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StagePrepMonthlyRefill> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*
                            FROM PrepMonthlyRefills p 
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT DISTINCT PatientPK, SiteCode, PrepNumber, RecordUUID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, PrepNumber, RecordUUID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.PrepNumber = s.PrepNumber  
                                    AND p.RecordUUID = s.RecordUUID
                                                                   
                            )
                        ";

                var existingRecords = await connection.QueryAsync<PrepMonthlyRefill>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string PrepNumber, string RecordUUID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.PrepNumber, x.RecordUUID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stagePrepmonthlyRefill
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.PrepNumber, x.RecordUUID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stagePrepmonthlyRefill, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stagePrepmonthlyRefill;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StagePrepMonthlyRefill> uniqueStageExtracts)
        {
            try
            {
                var latestRecordsDict = new Dictionary<string, StagePrepMonthlyRefill>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.PrepNumber}_{extract.RecordUUID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<PrepMonthlyRefill>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StagePrepMonthlyRefill> stageDrug, IEnumerable<PrepMonthlyRefill> existingRecords)
        {
            try
            {
                //Update existing data
                var stageDictionary = stageDrug
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.PrepNumber, x.RecordUUID })
                         .ToDictionary(
                             g => g.Key,
                             g => g.FirstOrDefault()
                         );

                foreach (var existingExtract in existingRecords)
                {
                    if (stageDictionary.TryGetValue(
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.PrepNumber, existingExtract.RecordUUID },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }

                var cons = _context.Database.GetConnectionString();
                var sql = $@"
                           UPDATE 
                                     PrepMonthlyRefills

                               SET                                  
                                    FacilityName = @FacilityName,
                                    HtsNumber = @HtsNumber,
                                    VisitID = @VisitID,
                                    RegimenPrescribed = @RegimenPrescribed,
                                    DispenseDate = @DispenseDate,
                                    Duration = @Duration,
                                    VisitDate = @VisitDate,
                                    BehaviorRiskAssessment = @BehaviorRiskAssessment,
                                    SexPartnerHIVStatus = @SexPartnerHIVStatus,
                                    SymptomsAcuteHIV = @SymptomsAcuteHIV,
                                    AdherenceCounsellingDone = @AdherenceCounsellingDone,
                                    ContraIndicationForPrEP = @ContraIndicationForPrEP,
                                    PrescribedPrepToday = @PrescribedPrepToday,
                                    NumberOfMonths = @NumberOfMonths,
                                    CondomsIssued = @CondomsIssued,
                                    NumberOfCondomsIssued = @NumberOfCondomsIssued,
                                    ClientGivenNextAppointment = @ClientGivenNextAppointment,
                                    AppointmentDate = @AppointmentDate,
                                    ReasonForFailureToGiveAppointment = @ReasonForFailureToGiveAppointment,
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
                                        ManifestId = @manifestId 
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