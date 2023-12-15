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

namespace DwapiCentral.Prep.Application.Commands;

public class MergePrepVisitCommand : IRequest<Result>
{
    public IEnumerable<PrepVisit> PrepVisits { get; set; }

    public MergePrepVisitCommand(IEnumerable<PrepVisit> prepVisits)
    {
        PrepVisits = prepVisits;
    }
}
public class MergePrepVisitCommandHandler : IRequestHandler<MergePrepVisitCommand, Result>
{
    private readonly IStagePrepVisitRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergePrepVisitCommandHandler(IStagePrepVisitRepository prepvisitrepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = prepvisitrepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePrepVisitCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.PrepVisits.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StagePrepVisit>>(request.PrepVisits);


        if (extracts.Any())
        {
            StandardizeClass<StagePrepVisit> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}

