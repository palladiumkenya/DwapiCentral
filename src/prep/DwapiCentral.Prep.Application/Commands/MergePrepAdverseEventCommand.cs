using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Models.Stage;
using DwapiCentral.Prep.Domain.Repository;
using DwapiCentral.Prep.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Application.Commands;

public class MergePrepAdverseEventCommand : IRequest<Result>
{
    public IEnumerable<PrepAdverseEvent> PrepAdverseEvents { get; set; }

    public MergePrepAdverseEventCommand(IEnumerable<PrepAdverseEvent> prepAdverseEvents)
    {
        PrepAdverseEvents = prepAdverseEvents;
    }
}
public class MergePrepAdverseEventCommandHandler : IRequestHandler<MergePrepAdverseEventCommand, Result>
{
    private readonly IStagePrepAdverseEventRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergePrepAdverseEventCommandHandler(IStagePrepAdverseEventRepository adverseEventRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = adverseEventRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePrepAdverseEventCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.PrepAdverseEvents.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StagePrepAdverseEvent>>(request.PrepAdverseEvents);


        if (extracts.Any())
        {
            StandardizeClass<StagePrepAdverseEvent> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}


