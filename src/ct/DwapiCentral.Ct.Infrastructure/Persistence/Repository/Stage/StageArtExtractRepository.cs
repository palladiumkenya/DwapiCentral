using System.Reflection;
using AutoMapper;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using log4net;


namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageArtExtractRepository :
        StageExtractRepository<StageArtExtract>, IStageArtExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;

        public StageArtExtractRepository(CtDbContext context, IMapper mapper,
            string stageName = nameof(StageArtExtract), string extractName = nameof(PatientArtExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}
