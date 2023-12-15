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

public class MergeMatVisitCommand : IRequest<Result>
{
    public IEnumerable<MatVisit> MatVisits { get; set; }

    public MergeMatVisitCommand(IEnumerable<MatVisit> matVisits)
    {
        MatVisits = matVisits;
    }
}
public class MergeMatVisitCommandHandler : IRequestHandler<MergeMatVisitCommand, Result>
{
    private readonly IStageMatVisitRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeMatVisitCommandHandler(IStageMatVisitRepository matVisitRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = matVisitRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeMatVisitCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.MatVisits.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageMatVisit>>(request.MatVisits);


        if (extracts.Any())
        {
            StandardizeClass<StageMatVisit> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}
