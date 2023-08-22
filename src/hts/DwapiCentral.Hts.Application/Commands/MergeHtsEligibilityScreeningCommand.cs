using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Model.Stage;
using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Hts.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Application.Commands;

public class MergeHtsEligibilityScreeningCommand : IRequest<Result>
{
    public IEnumerable<HtsEligibilityScreening> HtsEligibility { get; set; }

    public MergeHtsEligibilityScreeningCommand(IEnumerable<HtsEligibilityScreening> htsEligibility)
    {

        HtsEligibility = htsEligibility;
    }
}

public class MergeHtsEligibilityScreeningCommandHandler : IRequestHandler<MergeHtsEligibilityScreeningCommand, Result>
{
    private readonly IStageHtsEligibilityScreeningRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeHtsEligibilityScreeningCommandHandler(IStageHtsEligibilityScreeningRepository htsClientRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = htsClientRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeHtsEligibilityScreeningCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.HtsEligibility.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageHtsEligibilityScreening>>(request.HtsEligibility);


        if (extracts.Any())
        {
            StandardizeClass<StageHtsEligibilityScreening> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }

}
