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

public class MergeMnchLabCommand : IRequest<Result>
{
    public IEnumerable<MnchLab> MnchLabs { get; set; }

    public MergeMnchLabCommand(IEnumerable<MnchLab> mnchLabs)
    {
        MnchLabs = mnchLabs;
    }
}
public class MergeMnchLabCommandHandler : IRequestHandler<MergeMnchLabCommand, Result>
{
    private readonly IStageMnchLabRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeMnchLabCommandHandler(IStageMnchLabRepository mnchLabRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = mnchLabRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeMnchLabCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.MnchLabs.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageMnchLab>>(request.MnchLabs);


        if (extracts.Any())
        {
            StandardizeClass<StageMnchLab> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}

