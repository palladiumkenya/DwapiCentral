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

public class MergeMnchArtCommand : IRequest<Result>
{
    public IEnumerable<MnchArt> MnchArts { get; set; }

    public MergeMnchArtCommand(IEnumerable<MnchArt> mnchArts)
    {
        MnchArts = mnchArts;
    }
}
public class MergeMnchArtCommandHandler : IRequestHandler<MergeMnchArtCommand, Result>
{
    private readonly IStageMnchArtRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeMnchArtCommandHandler(IStageMnchArtRepository matVisitRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = matVisitRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeMnchArtCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.MnchArts.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageMnchArt>>(request.MnchArts);


        if (extracts.Any())
        {
            StandardizeClass<StageMnchArt> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}

