using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Model.Stage;
using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Hts.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Application.Commands;

public class MergeHtsTestKitCommand : IRequest<Result>
{
    public IEnumerable<HtsTestKit> TestKits { get; set; }

    public MergeHtsTestKitCommand(IEnumerable<HtsTestKit> testKits)
    {
        TestKits = testKits;
    }
}

public class MergeHtsTestKitCommandHandler : IRequestHandler<MergeHtsTestKitCommand, Result>
{
    private readonly IStageHtsTestKitRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeHtsTestKitCommandHandler(IStageHtsTestKitRepository htsClientRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = htsClientRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeHtsTestKitCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.TestKits.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageHtsTestKit>>(request.TestKits);


        if (extracts.Any())
        {
            StandardizeClass<StageHtsTestKit> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }

}
