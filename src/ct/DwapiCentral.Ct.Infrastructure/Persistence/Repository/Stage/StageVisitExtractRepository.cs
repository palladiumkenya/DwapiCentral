using System.Reflection;
using AutoMapper;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using log4net;


namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageVisitExtractRepository :
        StageExtractRepository<StageVisitExtract>, IStageVisitExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;

        public StageVisitExtractRepository(CtDbContext context, IMapper mapper,
            string stageName = nameof(StageVisitExtract), string extractName = nameof(PatientVisitExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}



