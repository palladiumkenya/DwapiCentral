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

public class MergeCwcVisitCommand : IRequest<Result>
{
    public IEnumerable<CwcVisit> CwcVisits { get; set; }

    public MergeCwcVisitCommand(IEnumerable<CwcVisit> cwcVisits)
    {
        CwcVisits = cwcVisits;
    }
}
public class MergeCwcVisitCommandHandler : IRequestHandler<MergeCwcVisitCommand, Result>
{
    private readonly IStageCwcVisitRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeCwcVisitCommandHandler(IStageCwcVisitRepository cwcVisitRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = cwcVisitRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeCwcVisitCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.CwcVisits.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageCwcVisit>>(request.CwcVisits);


        if (extracts.Any())
        {
            StandardizeClass<StageCwcVisit> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}


