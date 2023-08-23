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

public class MergeMnchImmunizationCommand : IRequest<Result>
{
    public IEnumerable<MnchImmunization> MnchImmunizations { get; set; }

    public MergeMnchImmunizationCommand(IEnumerable<MnchImmunization> mnchImmunizations)
    {
        MnchImmunizations = mnchImmunizations;
    }
}
public class MergeMnchImmunizationCommandHandler : IRequestHandler<MergeMnchImmunizationCommand, Result>
{
    private readonly IStageMnchImmunizationRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeMnchImmunizationCommandHandler(IStageMnchImmunizationRepository mnchImmunizationRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = mnchImmunizationRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeMnchImmunizationCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.MnchImmunizations.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageMnchImmunization>>(request.MnchImmunizations);


        if (extracts.Any())
        {
            StandardizeClass<StageMnchImmunization> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}

