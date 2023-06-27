using System.Reflection;
using AutoMapper;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using log4net;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageAllergiesChronicIllnessExtractRepository :
        StageExtractRepository<StageAllergiesChronicIllnessExtract>, IStageAllergiesChronicIllnessExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;

        public StageAllergiesChronicIllnessExtractRepository(CtDbContext context, IMapper mapper,
            string stageName = nameof(StageAllergiesChronicIllnessExtract), string extractName = nameof(AllergiesChronicIllnessExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}
