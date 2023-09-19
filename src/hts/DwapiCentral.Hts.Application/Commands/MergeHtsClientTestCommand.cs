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

public class MergeHtsClientTestCommand : IRequest<Result>
{
    public IEnumerable<HtsClientTest> ClientTests { get; set; }

    public MergeHtsClientTestCommand(IEnumerable<HtsClientTest> clientTests)
    {
        ClientTests = clientTests;
    }    
}
public class MergeHtsClientTestCommandHandler : IRequestHandler<MergeHtsClientTestCommand, Result>
{
    private readonly IStageHtsClientTestRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeHtsClientTestCommandHandler(IStageHtsClientTestRepository htsClientRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = htsClientRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeHtsClientTestCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.ClientTests.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageHtsClientTest>>(request.ClientTests);


        if (extracts.Any())
        {
            StandardizeClass<StageHtsClientTest> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}
