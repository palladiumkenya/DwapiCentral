using AutoMapper;
using DwapiCentral.Mnch.Domain.Model.Stage;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using DwapiCentral.Mnch.Infrastructure.Persistence.Context;
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
using Z.Dapper.Plus;
using DwapiCentral.Mnch.Domain.Events;
using Dapper;

namespace DwapiCentral.Mnch.Infrastructure.Persistence.Repository.Stage
{
    public class StageMnchEnrolmentRepository : IStageMnchEnrolmentRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly MnchDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageMnchEnrolmentRepository(MnchDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageMnchEnrolment)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageMnchEnrolment> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "MnchEnrolments" };
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

        private async Task MergeExtracts(Guid manifestId, List<StageMnchEnrolment> stageMnchEnrollment)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageMnchEnrolment> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Rest
                };
                var query = $@"
                            SELECT p.*

                            FROM MnchEnrolments p

                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT PatientPK, SiteCode, PatientMnchID
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        ManifestId = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, PatientMnchID
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.PatientMnchID = s.PatientMnchID                                   
                                                                   
                            )
                        ";

                var existingRecords = await connection.QueryAsync<MnchEnrolment>(query, queryParameters);

                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string PatientMnchID)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.PatientMnchID)));

                if (existingRecordsSet.Any())
                {

                    // Filter out duplicates from stageExtracts               
                    uniqueStageExtracts = stageMnchEnrollment
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.PatientMnchID)) && x.ManifestId == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageMnchEnrollment, existingRecords);

                }
                else
                {
                    uniqueStageExtracts = stageMnchEnrollment;
                }
                await InsertNewDataFromStaging(uniqueStageExtracts);


            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageMnchEnrolment> uniqueStageExtracts)
        {
            try
            {

                var latestRecordsDict = new Dictionary<string, StageMnchEnrolment>();

                foreach (var extract in uniqueStageExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.PatientMnchID}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<MnchEnrolment>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageMnchEnrolment> stageDrug, IEnumerable<MnchEnrolment> existingRecords)
        {
            try
            {
                //Update existing data
                var stageDictionary = stageDrug
                         .GroupBy(x => new { x.PatientPk, x.SiteCode, x.PatientMnchID })
                         .ToDictionary(
                             g => g.Key,
                             g => g.FirstOrDefault()
                         );

                foreach (var existingExtract in existingRecords)
                {
                    if (stageDictionary.TryGetValue(
                        new { existingExtract.PatientPk, existingExtract.SiteCode, existingExtract.PatientMnchID },
                        out var stageExtract)
                    )
                    {
                        _mapper.Map(stageExtract, existingExtract);
                    }
                }

                var cons = _context.Database.GetConnectionString();
                var sql = $@"
                           UPDATE 
                                     MnchEnrolments

                               SET                                  
                                    DateExtracted = @DateExtracted,                                   
                                    FacilityName = @FacilityName,
                                    ServiceType = @ServiceType,
                                    EnrollmentDateAtMnch = @EnrollmentDateAtMnch,
                                    MnchNumber = @MnchNumber,
                                    FirstVisitAnc = @FirstVisitAnc,
                                    Parity = @Parity,
                                    Gravidae = @Gravidae,
                                    LMP = @LMP,
                                    EDDFromLMP = @EDDFromLMP,
                                    HIVStatusBeforeANC = @HIVStatusBeforeANC,
                                    HIVTestDate = @HIVTestDate,
                                    PartnerHIVStatus = @PartnerHIVStatus,
                                    PartnerHIVTestDate = @PartnerHIVTestDate,
                                    BloodGroup = @BloodGroup,
                                    StatusAtMnch = @StatusAtMnch,
                                    Date_Created = @Date_Created,
                                    DateLastModified = @DateLastModified,
                                    Created = @Created,
                                    Updated = @Updated,
                                    Voided = @Voided   

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND PatientMnchID = @PatientMnchID";

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
