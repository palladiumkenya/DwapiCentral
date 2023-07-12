using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using log4net;

using Z.Dapper.Plus;

namespace DwapiCentral.Ct.Infrastructure.Persistence.Repository.Stage
{
    public abstract class StageExtractRepository<T> : IStageExtractRepository<T>
        where T : IStage

    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CtDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _stageName;
        private readonly string _extractName;

        public StageExtractRepository(CtDbContext context, IMapper mapper, string stageName, string extractName)
        {
            _context = context;
            _mapper = mapper;
            _stageName = stageName;
            _extractName = extractName;
        }

        public Task ClearSite(Guid facilityId, Guid manifestId)
        {
            throw new NotImplementedException();
        }

        public Task SyncStage(List<T> extracts, Guid manifestId)
        {
            throw new NotImplementedException();
        }

        
    }
}
