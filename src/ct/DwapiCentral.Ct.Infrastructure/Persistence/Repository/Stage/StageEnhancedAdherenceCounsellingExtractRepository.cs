using System.Reflection;
using AutoMapper;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage;
using log4net;

namespace PalladiumDwh.Infrastructure.Data.Repository.Stage
{
    public class StageEnhancedAdherenceCounsellingExtractRepository :
        StageExtractRepository<StageEnhancedAdherenceCounsellingExtract>, IStageEnhancedAdherenceCounsellingExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;

        public StageEnhancedAdherenceCounsellingExtractRepository(CtDbContext context, IMapper mapper,
            string stageName = nameof(StageEnhancedAdherenceCounsellingExtract), string extractName = nameof(EnhancedAdherenceCounsellingExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}
