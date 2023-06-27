using System.Reflection;
using AutoMapper;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using log4net;


namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageAdverseEventExtractRepository :
          StageExtractRepository<StageAdverseEventExtract>, IStageAdverseEventExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;

        public StageAdverseEventExtractRepository(CtDbContext context, IMapper mapper,
            string stageName = nameof(StageAdverseEventExtract), string extractName = nameof(PatientAdverseEventExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}
