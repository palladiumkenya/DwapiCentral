using System.Data;
using System.Reflection;
using AutoMapper;
using Dapper;
using DwapiCentral.Contracts.Common;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;
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
    public class StageVisitExtractRepository :IStageVisitExtractRepository
    {

        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageVisitExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageVisitExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

        public async Task SyncStage(List<StageVisitExtract> extracts, Guid manifestId)
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

                // assign > Assigned
                await AssignAll(manifestId, pks);

                // Merge
                await MergeExtracts(manifestId, extracts);


                await UpdateLivestage(manifestId, pks);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extracts.Count, ManifestId = manifestId, SiteCode = extracts.First().SiteCode, ExtractName = "PatientVisitExtract" };

                await _mediator.Publish(notification);
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageVisitExtract> stageVisits)
        {
            var cons = _context.Database.GetConnectionString();
            try
            {
                using var connection = new SqlConnection(cons);
                List<StageVisitExtract> uniqueStageExtracts;
                await connection.OpenAsync();

                var queryParameters = new
                {
                    manifestId,
                    livestage = LiveStage.Assigned                   
                };
                var query = $@"
                            SELECT p.*
                            FROM PatientVisitExtract p
                            WHERE EXISTS (
                                SELECT 1
                                FROM (
                                    SELECT DISTINCT PatientPK, SiteCode, RecordUUID, Mhash
                                    FROM {_stageName} WITH (NOLOCK)
                                    WHERE 
                                        LiveSession = @manifestId 
                                        AND LiveStage = @livestage
                                    GROUP BY PatientPK, SiteCode, RecordUUID, Mhash
                                ) s
                                WHERE p.PatientPk = s.PatientPK
                                    AND p.SiteCode = s.SiteCode
                                    AND p.RecordUUID = s.RecordUUID  
                                    AND p.Mhash = s.Mhash 
                                                                     
                            )
                        ";               
                
                var existingRecords = await connection.QueryAsync<PatientVisitExtract>(query, queryParameters);
                
                // Convert existing records to HashSet for duplicate checking
                var existingRecordsSet = new HashSet<(int PatientPK, int SiteCode, string RecordUUID,int Mhash)>(existingRecords.Select(x => (x.PatientPk, x.SiteCode, x.RecordUUID, x.Mhash)));

                if (existingRecordsSet.Any())               {                  
                                                   
                    uniqueStageExtracts = stageVisits
                        .Where(x => !existingRecordsSet.Contains((x.PatientPk, x.SiteCode, x.RecordUUID,x.Mhash)) && x.LiveSession == manifestId)
                        .ToList();

                    await UpdateCentralDataWithStagingData(stageVisits,existingRecords,manifestId);

                }
                else
                {
                    uniqueStageExtracts = stageVisits;
                }

                await InsertNewDataFromStaging(uniqueStageExtracts,manifestId);

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }

        }

        private async Task InsertNewDataFromStaging(List<StageVisitExtract> uniqueStageExtracts, Guid manifestId)
        {
            try
            {
                var sortedExtracts = uniqueStageExtracts.OrderByDescending(e => e.Date_Created).ToList();
                var latestRecordsDict = new Dictionary<string, StageVisitExtract>();

                foreach (var extract in sortedExtracts)
                {
                    var key = $"{extract.PatientPk}_{extract.SiteCode}_{extract.RecordUUID}_{extract.Mhash}";

                    if (!latestRecordsDict.ContainsKey(key))
                    {
                        latestRecordsDict[key] = extract;
                    }
                }

                var filteredExtracts = latestRecordsDict.Values.ToList();
                var mappedExtracts = _mapper.Map<List<PatientVisitExtract>>(filteredExtracts);
                _context.Database.GetDbConnection().BulkInsert(mappedExtracts);
            }
            catch(Exception ex)
            {
                Log.Error(ex);
                var notification = new OnErrorEvent { ExtractName = "PatientVisitExtract", ManifestId = manifestId, SiteCode = uniqueStageExtracts.First().SiteCode, message = ex.Message };
                await _mediator.Publish(notification);
                throw;
            }
        }

        private async Task UpdateCentralDataWithStagingData(List<StageVisitExtract> stageVisits, IEnumerable<PatientVisitExtract> existingRecords, Guid manifestId)
        {
                try
                {
                var centraldata = stageVisits
                                .Select(_mapper.Map<StageVisitExtract, PatientVisitExtract>)
                                .ToList();

                centraldata = centraldata
                                .GroupBy(x => new { x.PatientPk, x.SiteCode,x.RecordUUID, x.Mhash })
                                .Select(g => g.First())
                                .ToList();

                var existingIds = existingRecords
                                .Select(x => new { x.PatientPk, x.SiteCode, x.RecordUUID, x.Mhash })
                                .ToHashSet();

                var recordsToUpdate = centraldata
                                .Where(x => existingIds.Contains(new { x.PatientPk, x.SiteCode, x.RecordUUID, x.Mhash }))
                                .ToList();


                const int maxRetries = 3;

                    for (var retry = 0; retry < maxRetries; retry++)
                    {
                        try
                    {
                        _context.Database.GetDbConnection().BulkUpdate(recordsToUpdate);
                    }
                        catch (SqlException ex)
                        {
                            if (ex.Number == 1205)
                            {
                                _context.Database.GetDbConnection().BulkMerge(recordsToUpdate);
                                await Task.Delay(100);
                            }
                            else
                            {
                                Log.Error(ex);
                                var notification = new OnErrorEvent { ExtractName = "PatientVisitExtract", ManifestId = manifestId, SiteCode = existingRecords.First().SiteCode, message = ex.Message };
                                await _mediator.Publish(notification);
                                throw;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    throw;
                }

                //var cons = _context.Database.GetConnectionString();
                //using (var connection = new SqlConnection(cons))
                //{
                //    await connection.OpenAsync();

                //    using (var transaction = connection.BeginTransaction())
                //    {
                //        const int maxRetries = 3; 

                //        for (var retry = 0; retry < maxRetries; retry++)
                //        {
                //            try
                //            {



                //                var sql = $@"
                //           UPDATE 
                //                     pve 

                //               SET
                //                    pve.VisitID = sve.VisitID,
                //                    pve.VisitDate = sve.VisitDate,                                    
                //                    pve.Service = sve.Service,
                //                    pve.VisitType = sve.VisitType,
                //                    pve.WHOStage = sve.WHOStage,
                //                    pve.WABStage = sve.WABStage,
                //                    pve.Pregnant = sve.Pregnant,
                //                    pve.LMP = sve.LMP,
                //                    pve.EDD = sve.EDD,
                //                    pve.Height = sve.Height,
                //                    pve.Weight = sve.Weight,
                //                    pve.BP = sve.BP,
                //                    pve.OI = sve.OI,
                //                    pve.OIDate = sve.OIDate,
                //                    pve.SubstitutionFirstlineRegimenDate = sve.SubstitutionFirstlineRegimenDate,
                //                    pve.SubstitutionFirstlineRegimenReason = sve.SubstitutionFirstlineRegimenReason,
                //                    pve.SubstitutionSecondlineRegimenDate = sve.SubstitutionSecondlineRegimenDate,
                //                    pve.SubstitutionSecondlineRegimenReason = sve.SubstitutionSecondlineRegimenReason,
                //                    pve.SecondlineRegimenChangeDate = sve.SecondlineRegimenChangeDate,
                //                    pve.SecondlineRegimenChangeReason = sve.SecondlineRegimenChangeReason,
                //                    pve.Adherence = sve.Adherence,
                //                    pve.AdherenceCategory = sve.AdherenceCategory,
                //                    pve.FamilyPlanningMethod = sve.FamilyPlanningMethod,
                //                    pve.PwP = sve.PwP,
                //                    pve.GestationAge = sve.GestationAge,
                //                    pve.NextAppointmentDate = sve.NextAppointmentDate,
                //                    pve.StabilityAssessment = sve.StabilityAssessment,
                //                    pve.DifferentiatedCare = sve.DifferentiatedCare,
                //                    pve.PopulationType = sve.PopulationType,
                //                    pve.KeyPopulationType = sve.KeyPopulationType,
                //                    pve.VisitBy = sve.VisitBy,
                //                    pve.Temp = sve.Temp,
                //                    pve.PulseRate = sve.PulseRate,
                //                    pve.RespiratoryRate = sve.RespiratoryRate,
                //                    pve.OxygenSaturation = sve.OxygenSaturation,
                //                    pve.Muac = sve.Muac,
                //                    pve.NutritionalStatus = sve.NutritionalStatus,
                //                    pve.EverHadMenses = sve.EverHadMenses,
                //                    pve.Breastfeeding = sve.Breastfeeding,
                //                    pve.Menopausal = sve.Menopausal,
                //                    pve.NoFPReason = sve.NoFPReason,
                //                    pve.ProphylaxisUsed = sve.ProphylaxisUsed,
                //                    pve.CTXAdherence = sve.CTXAdherence,
                //                    pve.CurrentRegimen = sve.CurrentRegimen,
                //                    pve.HCWConcern = sve.HCWConcern,
                //                    pve.TCAReason = sve.TCAReason,
                //                    pve.ClinicalNotes = sve.ClinicalNotes,
                //                    pve.GeneralExamination = sve.GeneralExamination,
                //                    pve.SystemExamination = sve.SystemExamination,
                //                    pve.Skin = sve.Skin,
                //                    pve.Eyes = sve.Eyes,
                //                    pve.ENT = sve.ENT,
                //                    pve.Chest = sve.Chest,
                //                    pve.CVS = sve.CVS,
                //                    pve.Abdomen = sve.Abdomen,
                //                    pve.CNS = sve.CNS,
                //                    pve.Genitourinary = sve.Genitourinary,
                //                    pve.RefillDate = sve.RefillDate,
                //                    pve.Date_Created = sve.Date_Created,
                //                    pve.DateLastModified = sve.DateLastModified,
                //                    pve.DateExtracted = sve.DateExtracted,
                //                    pve.Created = sve.Created,
                //                    pve.Updated = sve.Updated,
                //                    pve.Voided = sve.Voided                        

                //        FROM PatientVisitExtract pve
                //        JOIN {_stageName} spv ON pve.RecordUUID = spv.RecordUUID
                //        WHERE pve.RecordUUID = @RecordUUID";



                //                await connection.ExecuteAsync(sql, existingRecords, transaction);
                //                    transaction.Commit();

                //                break; 
                //            }
                //            catch (SqlException ex)
                //            {
                //                if (ex.Number == 1205) 
                //                {

                //                    await Task.Delay(100);
                //                }
                //                else
                //                {
                //                    transaction.Rollback();
                //                    throw; 
                //                }
                //            }
                //        }
                //    }
                //}           
           
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

        private async Task UpdateLivestage(Guid manifestId, List<Guid> ids)
        {

            var cons = _context.Database.GetConnectionString();

            var sql = $@"
                            UPDATE 
                                    {_stageName}
                            SET 
                                    LiveStage= @nextlivestage 
                            
                            WHERE 
                                    LiveSession = @manifestId AND 
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
                        livestage = LiveStage.Assigned,
                        nextlivestage = LiveStage.Merged,
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
                                        LiveSession = @manifestId 
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



