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

public class MergeHtsClientLinkageCommand : IRequest<Result>
{
    public IEnumerable<HtsClientLinkage> ClientLinkage { get; set; }

    public MergeHtsClientLinkageCommand(IEnumerable<HtsClientLinkage> clientLinkage)
    {
        ClientLinkage = clientLinkage;
    }
}

public class MergeHtsClientLinkageCommandHandler : IRequestHandler<MergeHtsClientLinkageCommand, Result>
{
    private readonly IStageHtsClientLinkageRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeHtsClientLinkageCommandHandler(IStageHtsClientLinkageRepository htsClientRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = htsClientRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeHtsClientLinkageCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.ClientLinkage.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageHtsClientLinkage>>(request.ClientLinkage);


        if (extracts.Any())
        {
            StandardizeClass<StageHtsClientLinkage> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }

}
