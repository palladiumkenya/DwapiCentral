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

public class MergePncVisitCommand : IRequest<Result>
{
    public IEnumerable<PncVisit> PncVisits { get; set; }

    public MergePncVisitCommand(IEnumerable<PncVisit> pncVisits)
    {
        PncVisits = pncVisits;
    }
}
public class MergePncVisitCommandHandler : IRequestHandler<MergePncVisitCommand, Result>
{
    private readonly IStagePncVisitRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergePncVisitCommandHandler(IStagePncVisitRepository pncVisitRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = pncVisitRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePncVisitCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.PncVisits.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StagePncVisit>>(request.PncVisits);


        if (extracts.Any())
        {
            StandardizeClass<StagePncVisit> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}

