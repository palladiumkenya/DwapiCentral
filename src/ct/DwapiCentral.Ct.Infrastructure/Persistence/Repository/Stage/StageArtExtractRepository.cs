using System.Data;
using System.Reflection;
using AutoMapper;
using Dapper;
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

                // assign > Assigned
                await AssignAll(manifestId, extracts.Select(x => x.Id).ToList());
                
                // Merge
                await MergeExtracts(manifestId, extracts.Select(x =>new StageArtExtract{PatientPk= x.PatientPk,SiteCode= x.SiteCode,LastARTDate= x.LastARTDate}).ToList());

                // assign > Merged
               // await SmartMarkRegister(manifestId, extracts.Select(x => x.Id).ToList());

            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private async Task MergeExtracts(Guid manifestId, List<StageArtExtract> stageArt)
        {
            //Retrieve existing visits from the central table
            List<PatientArtExtract> extracts = await GetExistingData();

            //Det if it's first Time sending Data
            bool isFirstTime = extracts.Count == 0;

            //Dictionary to keep track of uniqueData
            Dictionary<string,PatientArtExtract> uniqueData = new Dictionary<string,PatientArtExtract>();

            foreach(StageArtExtract stageArtExtract in stageArt) {

                //gen a unique identifier for visit
                string uniqueIdentifier = GetUniqueIdentifier(stageArtExtract);

                //check if data exists in dictionary
                if(uniqueData.TryGetValue(uniqueIdentifier, out PatientArtExtract existingExtracts)){

                }
                else if(isFirstTime)
                {
                    uniqueData.Add(uniqueIdentifier, CreateNewExtract(stageArt));
                }
            
            
            }
            // Merge the unique visits from the staging table with the existing visits from the central table
            List<PatientArtExtract> mergedArt = extracts.Concat(uniqueData.Values).ToList();

            // Save the merged visits to the central table
            await SaveMergedVisitsToCentralTable(mergedArt);




           
        }

        private async Task SaveMergedVisitsToCentralTable(List<PatientArtExtract> mergedArtExtracts)
        {
            _context.Database.GetDbConnection().BulkMerge(mergedArtExtracts);
        }

        private PatientArtExtract CreateNewExtract(List<StageArtExtract> stageArt)
        {
            
            PatientArtExtract newArt= _mapper.Map<PatientArtExtract>(stageArt);

           

            return newArt;
        }

        private string GetUniqueIdentifier(StageArtExtract stageArtExtract)
        {
            return $"{stageArtExtract.PatientPk}_{stageArtExtract.SiteCode}_{stageArtExtract.LastARTDate}";
        }

        private async Task<List<PatientArtExtract>> GetExistingData()
        {
            var cons = _context.Database.GetConnectionString();

            using (var connection = new SqlConnection(cons))
            {
                var query = "SELECT * FROM PatientArtExtracts";

                return (await connection.QueryAsync<PatientArtExtract>(query)).ToList();
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
