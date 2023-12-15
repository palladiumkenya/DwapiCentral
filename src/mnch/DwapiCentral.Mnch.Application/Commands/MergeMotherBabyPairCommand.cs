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

public class MergeMotherBabyPairCommand : IRequest<Result>
{
    public IEnumerable<MotherBabyPair> MnchLabs { get; set; }

    public MergeMotherBabyPairCommand(IEnumerable<MotherBabyPair> mnchLabs)
    {
        MnchLabs = mnchLabs;
    }
}
public class MergeMotherBabyPairCommandHandler : IRequestHandler<MergeMotherBabyPairCommand, Result>
{
    private readonly IStageMotherBabyPairRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeMotherBabyPairCommandHandler(IStageMotherBabyPairRepository motherBabyPairRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = motherBabyPairRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeMotherBabyPairCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.MnchLabs.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageMotherBabyPair>>(request.MnchLabs);


        if (extracts.Any())
        {
            StandardizeClass<StageMotherBabyPair> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}
