using System.Reflection;
using AutoMapper;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using log4net;


namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageGbvScreeningExtractRepository :
        StageExtractRepository<StageGbvScreeningExtract>, IStageGbvScreeningExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;

        public StageGbvScreeningExtractRepository(CtDbContext context, IMapper mapper,
            string stageName = nameof(StageGbvScreeningExtract), string extractName = nameof(GbvScreeningExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}
