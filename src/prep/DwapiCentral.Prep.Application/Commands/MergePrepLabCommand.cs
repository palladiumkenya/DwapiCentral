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

public class MergePrepLabCommand : IRequest<Result>
{
    public IEnumerable<PrepLab> PrepLabs { get; set; }

    public MergePrepLabCommand(IEnumerable<PrepLab> prepLabs)
    {
        PrepLabs = prepLabs;
    }
}
public class MergePrepLabCommandHandler : IRequestHandler<MergePrepLabCommand, Result>
{
    private readonly IStagePrepLabRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergePrepLabCommandHandler(IStagePrepLabRepository prepLabrepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = prepLabrepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePrepLabCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.PrepLabs.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StagePrepLab>>(request.PrepLabs);


        if (extracts.Any())
        {
            StandardizeClass<StagePrepLab> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}

