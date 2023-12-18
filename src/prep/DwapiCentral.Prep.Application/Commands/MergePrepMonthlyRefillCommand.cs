using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Prep.Domain.Models.Stage;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DwapiCentral.Prep.Domain.Repository;

namespace DwapiCentral.Prep.Application.Commands
{
    public class MergePrepMonthlyRefillCommand : IRequest<Result>
    {

        public IEnumerable<PrepMonthlyRefill> PrepMonthlyRefill { get; set; }

        public MergePrepMonthlyRefillCommand(IEnumerable<PrepMonthlyRefill> prepMonthlyRefill)
        {
            PrepMonthlyRefill = prepMonthlyRefill;
        }
    }
    public class MergePrepMonthlyRefillCommandHandler : IRequestHandler<MergePrepMonthlyRefillCommand, Result>
    {
        private readonly IStagePrepMonthlyRefillRepository _Repository;
        private readonly IManifestRepository _manifestRepository;
        private readonly IMapper _mapper;


        public MergePrepMonthlyRefillCommandHandler(IStagePrepMonthlyRefillRepository preprepository, IManifestRepository manifestRepository, IMapper mapper)
        {
            _Repository = preprepository;
            _manifestRepository = manifestRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(MergePrepMonthlyRefillCommand request, CancellationToken cancellationToken)
        {
            var manifestId = await _manifestRepository.GetManifestId(request.PrepMonthlyRefill.FirstOrDefault().SiteCode);

            var extracts = _mapper.Map<List<StagePrepMonthlyRefill>>(request.PrepMonthlyRefill);


            if (extracts.Any())
            {
                StandardizeClass<StagePrepMonthlyRefill> standardizer = new(extracts, manifestId);
                standardizer.StandardizeExtracts();

            }
            //stage
            await _Repository.SyncStage(extracts, manifestId);


            return Result.Success();
        }
    }
}



