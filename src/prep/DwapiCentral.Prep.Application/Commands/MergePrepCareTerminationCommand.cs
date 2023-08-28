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

public class MergePrepCareTerminationCommand : IRequest<Result>
{
    public IEnumerable<PrepCareTermination> PrepCareTerminations { get; set; }

    public MergePrepCareTerminationCommand(IEnumerable<PrepCareTermination> prepCareTerminations)
    {
        PrepCareTerminations = prepCareTerminations;
    }
}
public class MergePrepCareTerminationCommandHandler : IRequestHandler<MergePrepCareTerminationCommand, Result>
{
    private readonly IStagePrepCareTerminationRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergePrepCareTerminationCommandHandler(IStagePrepCareTerminationRepository prepCtrepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = prepCtrepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePrepCareTerminationCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.PrepCareTerminations.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StagePrepCareTermination>>(request.PrepCareTerminations);


        if (extracts.Any())
        {
            StandardizeClass<StagePrepCareTermination> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}
