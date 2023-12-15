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

public class MergePrepPharmacyCommand : IRequest<Result>
{
    public IEnumerable<PrepPharmacy> PrepPharmacies { get; set; }

    public MergePrepPharmacyCommand(IEnumerable<PrepPharmacy> prepPharmacies)
    {
        PrepPharmacies = prepPharmacies;
    }
}
public class MergePrepPharmacyCommandHandler : IRequestHandler<MergePrepPharmacyCommand, Result>
{
    private readonly IStagePrepPharmacyRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergePrepPharmacyCommandHandler(IStagePrepPharmacyRepository prepPharmacyrepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = prepPharmacyrepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePrepPharmacyCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.PrepPharmacies.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StagePrepPharmacy>>(request.PrepPharmacies);


        if (extracts.Any())
        {
            StandardizeClass<StagePrepPharmacy> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}


