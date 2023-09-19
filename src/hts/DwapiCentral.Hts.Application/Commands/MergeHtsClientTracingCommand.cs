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

public class MergeHtsClientTracingCommand : IRequest<Result>
{
    public IEnumerable<HtsClientTracing> ClientTracing { get; set;} 

    public MergeHtsClientTracingCommand(IEnumerable<HtsClientTracing> clientTracing)
    {
        ClientTracing = clientTracing;
    }
}

public class MergeHtsClientTracingCommandHandler : IRequestHandler<MergeHtsClientTracingCommand, Result>
{
    private readonly IStageHtsClientTracingRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeHtsClientTracingCommandHandler(IStageHtsClientTracingRepository htsClientRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = htsClientRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeHtsClientTracingCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.ClientTracing.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageHtsClientTracing>>(request.ClientTracing);


        if (extracts.Any())
        {
            StandardizeClass<StageHtsClientTracing> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }

}
