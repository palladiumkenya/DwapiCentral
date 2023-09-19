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

public class MergeHtsClientsCommand : IRequest<Result>
{
    public IEnumerable<HtsClient> Clients { get; set; }

    public MergeHtsClientsCommand(IEnumerable<HtsClient> clients)
    {
        Clients = clients;
    }
}

public class MergeHtsClientsCommandHandler : IRequestHandler<MergeHtsClientsCommand, Result>
{
    private readonly IStageHtsClientRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeHtsClientsCommandHandler(IStageHtsClientRepository htsClientRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = htsClientRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeHtsClientsCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.Clients.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageHtsClient>>(request.Clients);


        if (extracts.Any())
        {
            StandardizeClass<StageHtsClient> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts,manifestId);


        return Result.Success();
    }
}
