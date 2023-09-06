using Dapper;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Domain.Enums;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository
{
    public class EnhancedAdherenceCounsellingRepository : IEnhancedAdherenceCounsellingRepository
    {
        private readonly CtDbContext _context;
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public EnhancedAdherenceCounsellingRepository(CtDbContext context, IMediator mediator, IManifestRepository manifestRepository)
        {
            _context = context;
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        public async Task<EnhancedAdherenceCounsellingExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID)
        {
            // If not cached, retrieve the record from the database
            var query = "SELECT * FROM EnhancedAdherenceCounsellingExtract WHERE PatientPK = @PatientPK AND SiteCode = @SiteCode AND RecordUUID = @RecordUUID";

            var patientExtract = await _context.Database.GetDbConnection().QueryFirstOrDefaultAsync<EnhancedAdherenceCounsellingExtract>(query, new { patientPK, siteCode, recordUUID });

            return patientExtract;

        }

        public async Task InsertExtract(List<EnhancedAdherenceCounsellingExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();
                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                _context.Database.GetDbConnection().BulkInsert(patientExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "EnhancedAdherenceCounsellingExtract" };
                await _mediator.Publish(notification);

                connection.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk Insert of EnhancedAdherenceCounsellingExtract.");
            }
        }

        public async Task UpdateExtract(List<EnhancedAdherenceCounsellingExtract> patientExtract)
        {
            try
            {
                var cons = _context.Database.GetConnectionString();


                var sql = $@"
                           UPDATE 
                                     EnhancedAdherenceCounsellingExtract

                               SET                                  
                                    VisitID = @VisitID,
                                    VisitDate = @VisitDate,
                                    SessionNumber = @SessionNumber,
                                    DateOfFirstSession = @DateOfFirstSession,
                                    PillCountAdherence = @PillCountAdherence,
                                    MMAS4_1 = @MMAS4_1,
                                    MMAS4_2 = @MMAS4_2,
                                    MMAS4_3 = @MMAS4_3,
                                    MMAS4_4 = @MMAS4_4,
                                    MMSA8_1 = @MMSA8_1,
                                    MMSA8_2 = @MMSA8_2,
                                    MMSA8_3 = @MMSA8_3,
                                    MMSA8_4 = @MMSA8_4,
                                    MMSAScore = @MMSAScore,
                                    EACRecievedVL = @EACRecievedVL,
                                    EACVL = @EACVL,
                                    EACVLConcerns = @EACVLConcerns,
                                    EACVLThoughts = @EACVLThoughts,
                                    EACWayForward = @EACWayForward,
                                    EACCognitiveBarrier = @EACCognitiveBarrier,
                                    EACBehaviouralBarrier_1 = @EACBehaviouralBarrier_1,
                                    EACBehaviouralBarrier_2 = @EACBehaviouralBarrier_2,
                                    EACBehaviouralBarrier_3 = @EACBehaviouralBarrier_3,
                                    EACBehaviouralBarrier_4 = @EACBehaviouralBarrier_4,
                                    EACBehaviouralBarrier_5 = @EACBehaviouralBarrier_5,
                                    EACEmotionalBarriers_1 = @EACEmotionalBarriers_1,
                                    EACEmotionalBarriers_2 = @EACEmotionalBarriers_2,
                                    EACEconBarrier_1 = @EACEconBarrier_1,
                                    EACEconBarrier_2 = @EACEconBarrier_2,
                                    EACEconBarrier_3 = @EACEconBarrier_3,
                                    EACEconBarrier_4 = @EACEconBarrier_4,
                                    EACEconBarrier_5 = @EACEconBarrier_5,
                                    EACEconBarrier_6 = @EACEconBarrier_6,
                                    EACEconBarrier_7 = @EACEconBarrier_7,
                                    EACEconBarrier_8 = @EACEconBarrier_8,
                                    EACReviewImprovement = @EACReviewImprovement,
                                    EACReviewMissedDoses = @EACReviewMissedDoses,
                                    EACReviewStrategy = @EACReviewStrategy,
                                    EACReferral = @EACReferral,
                                    EACReferralApp = @EACReferralApp,
                                    EACReferralExperience = @EACReferralExperience,
                                    EACHomevisit = @EACHomevisit,
                                    EACAdherencePlan = @EACAdherencePlan,
                                    EACFollowupDate = @EACFollowupDate,
                                    Date_Created = @Date_Created,
                                    DateLastModified = @DateLastModified,
                                    DateExtracted = @DateExtracted,
                                    Created = @Created,
                                    Updated = @Updated,
                                    Voided = @Voided                          

                             WHERE  PatientPk = @PatientPK
                                    AND SiteCode = @SiteCode
                                    AND RecordUUID = @RecordUUID";



                using var connection = new SqlConnection(cons);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                await connection.ExecuteAsync(sql, patientExtract);

                var manifestId = await _manifestRepository.GetManifestId(patientExtract.First().SiteCode);

                connection.Close();

                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = patientExtract.Count, ManifestId = manifestId, SiteCode = patientExtract.First().SiteCode, ExtractName = "EnhancedAdherenceCounsellingExtract", UploadMode = UploadMode.DifferentialLoad };
                await _mediator.Publish(notification);


            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during bulk update of EnhancedAdherenceCounsellingExtract.");

            }
        }

    }
}
