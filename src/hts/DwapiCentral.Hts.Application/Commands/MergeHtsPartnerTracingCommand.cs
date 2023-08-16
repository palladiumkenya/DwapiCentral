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

public class MergeHtsPartnerTracingCommand : IRequest<Result>
{
    public IEnumerable<HtsPartnerTracing> PartnerTracing { get; set; }

    public MergeHtsPartnerTracingCommand(IEnumerable<HtsPartnerTracing> partnertracing)
    {

        PartnerTracing = partnertracing;
    }
}

public class MergeHtsPartnerTracingCommandHandler : IRequestHandler<MergeHtsPartnerTracingCommand, Result>
{
    private readonly IStageHtsPartnerTracingRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeHtsPartnerTracingCommandHandler(IStageHtsPartnerTracingRepository htsClientRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = htsClientRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeHtsPartnerTracingCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.PartnerTracing.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageHtsPartnerTracing>>(request.PartnerTracing);


        if (extracts.Any())
        {
            StandardizeClass<StageHtsPartnerTracing> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }

}
