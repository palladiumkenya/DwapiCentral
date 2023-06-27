using System.Reflection;
using AutoMapper;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using log4net;


namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public class StageContactListingExtractRepository :
        StageExtractRepository<StageContactListingExtract>, IStageContactListingExtractRepository
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;

        public StageContactListingExtractRepository(CtDbContext context, IMapper mapper,
            string stageName = nameof(StageContactListingExtract), string extractName = nameof(ContactListingExtract))
            : base(context, mapper, stageName, extractName)
        {

        }
    }
}
