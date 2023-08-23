using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Mnch.Domain.Model.Stage;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DwapiCentral.Mnch.Domain.Repository;

namespace DwapiCentral.Mnch.Application.Commands;

public class MergeHeiExtractCommand : IRequest<Result>
{
    public IEnumerable<HeiExtract> HeiExtracts { get; set; }

    public MergeHeiExtractCommand(IEnumerable<HeiExtract> heiExtracts)
    {
        HeiExtracts = heiExtracts;
    }
}
public class MergeHeiExtractCommandHandler : IRequestHandler<MergeHeiExtractCommand, Result>
{
    private readonly IStageHeiExtractRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeHeiExtractCommandHandler(IStageHeiExtractRepository heiRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = heiRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeHeiExtractCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.HeiExtracts.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageHeiExtract>>(request.HeiExtracts);


        if (extracts.Any())
        {
            StandardizeClass<StageHeiExtract> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}



