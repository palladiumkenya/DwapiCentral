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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageArtExtractRepository : IStageArtExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly string _stageName;

        public StageArtExtractRepository(CtDbContext context, IMapper mapper, IMediator mediator, string stageName = $"{nameof(StageAdverseEventExtract)}s")
        {
            _context = context;
            _mapper = mapper;
            _mediator = mediator;
            _stageName = stageName;
        }

     
        public async Task SyncStage(List<StageArtExtract> extracts, Guid manifestId)
        {
            try
            {
                // stage > Rest
                _context.Database.GetDbConnection().BulkInsert(extracts);

                var notification = new ExtractsReceivedEvent { TotalExtractsCount = extracts.Count, SiteCode = extracts.First().SiteCode, ExtractName = "PatientArtExtract" };
                await _mediator.Publish(notification);


                // assign > Assigned
                await AssignAll(manifestId, extracts.Select(x => x.Id).ToList());
                
                // Merge
                await MergeExtracts(manifestId, extracts.Select(x =>new StageArtExtract{PatientPk= x.PatientPk,SiteCode= x.SiteCode,LastARTDate= x.LastARTDate}).ToList());

                // assign > Merged
               //await SmartMarkRegister(manifestId, extracts.Select(x => x.Id).ToList());

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageArtExtract> stageArt)
        {
            var cons = _context.Database.GetConnectionString();

            // Merge data from staging table to central table using Dapper Plus
            var sql = $@"
                MERGE INTO PatientArtExtracts AS Target
                USING (
                    SELECT 
                             Id,PatientPk, SiteCode, LastARTDate,LastVisit,DOB,AgeEnrollment,
							 AgeARTStart,AgeLastVisit,RegistrationDate,Gender,PatientSource,StartARTDate,
							 PreviousARTStartDate,PreviousARTRegimen,StartARTAtThisFacility,StartRegimen,StartRegimenLine,
							 LastRegimen,LastRegimenLine,Duration,ExpectedReturn,Provider,ExitReason,ExitDate,
							 PreviousARTUse,PreviousARTPurpose,DateLastUsed,DateCreated,DateLastModified,DateExtracted,
							 Created,Updated,Voided
                    FROM 
                             StageArtExtracts
                    WHERE
                            LiveSession = @manifestId AND 
                            LiveStage = @livestage 
                          
                ) AS Source
                ON 
                                    Target.PatientPk = Source.PatientPk
                AND 
                                    Target.SiteCode = Source.SiteCode
                AND 
                                    Target.LastARTDate = Source.LastARTDate
                WHEN MATCHED THEN
                UPDATE SET
                                                    LastVisit=Source.LastVisit,
													DOB=Source.DOB,
													AgeEnrollment=Source.AgeEnrollment,
													AgeARTStart=Source.AgeARTStart,
													AgeLastVisit=Source.AgeLastVisit,
													RegistrationDate=Source.RegistrationDate,
													Gender=Source.Gender,
													PatientSource=Source.PatientSource,
													StartARTDate=Source.StartARTDate,
													PreviousARTStartDate=Source.PreviousARTStartDate,
													PreviousARTRegimen=Source.PreviousARTRegimen,
													StartARTAtThisFacility=Source.StartARTAtThisFacility,
													StartRegimen=Source.StartRegimen,
													StartRegimenLine=Source.StartRegimenLine,
													LastRegimen=Source.LastRegimen,
													LastRegimenLine=Source.LastRegimenLine,
													Duration=Source.Duration,
													ExpectedReturn=Source.ExpectedReturn,
													Provider=Source.Provider,
													ExitReason=Source.ExitReason,
													ExitDate=Source.ExitDate,
													PreviousARTUse=Source.PreviousARTUse,
													PreviousARTPurpose=Source.PreviousARTPurpose,
													DateLastUsed=Source.DateLastUsed,
													DateCreated=Source.DateCreated,
													DateLastModified=Source.DateLastModified,
													DateExtracted=Source.DateExtracted,
													Created=Source.Created,
													Updated=Source.Updated,
													Voided=Source.Voided
                WHEN NOT MATCHED THEN
                INSERT 
                             (Id,PatientPk, SiteCode, LastARTDate,LastVisit,DOB,AgeEnrollment,
							 AgeARTStart,AgeLastVisit,RegistrationDate,Gender,PatientSource,StartARTDate,
							 PreviousARTStartDate,PreviousARTRegimen,StartARTAtThisFacility,StartRegimen,StartRegimenLine,
							 LastRegimen,LastRegimenLine,Duration,ExpectedReturn,Provider,ExitReason,ExitDate,
							 PreviousARTUse,PreviousARTPurpose,DateLastUsed,DateCreated,DateLastModified,DateExtracted,
							 Created,Updated,Voided)
                VALUES 
                             (Source.Id, Source.PatientPk, Source.SiteCode, Source.LastARTDate,Source.LastVisit,Source.DOB,Source.AgeEnrollment,
							 Source.AgeARTStart,Source.AgeLastVisit,Source.RegistrationDate,Source.Gender,Source.PatientSource,Source.StartARTDate,
							 Source.PreviousARTStartDate,Source.PreviousARTRegimen,Source.StartARTAtThisFacility,Source.StartRegimen,Source.StartRegimenLine,
							 Source.LastRegimen,Source.LastRegimenLine,Source.Duration,Source.ExpectedReturn,Source.Provider,Source.ExitReason,Source.ExitDate,
							 Source.PreviousARTUse,Source.PreviousARTPurpose,Source.DateLastUsed,Source.DateCreated,Source.DateLastModified,Source.DateExtracted,
							 Source.Created,Source.Updated,Source.Voided);";

            var deleteQuery = $@"
                WITH CTE AS (
                    SELECT ROW_NUMBER() OVER (
                        PARTITION BY PatientPk, SiteCode, LastARTDate
                        ORDER BY LastARTDate DESC) AS RowNumber
                    FROM PatientArtExtracts
                )
                DELETE FROM CTE WHERE RowNumber > 1;";

            try
                {
                    using (var connection = new SqlConnection(cons))
                    {
                        if (connection.State != ConnectionState.Open)
                            connection.Open();
                        await connection.ExecuteAsync($"{sql}",
                            new { manifestId, livestage = LiveStage.Assigned }, null, 0);

                       await connection.ExecuteAsync($"{deleteQuery}");

                }
                }
                catch (Exception e)
                {
                    Log.Error(e);
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
