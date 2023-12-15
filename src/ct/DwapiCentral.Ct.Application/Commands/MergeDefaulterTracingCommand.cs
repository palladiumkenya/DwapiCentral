using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands;

public class MergeDefaulterTracingCommand : IRequest<Result>
{
    public DefaulterTracingSourceBag DefaulterTracingExtracts { get; set; }

    public MergeDefaulterTracingCommand(DefaulterTracingSourceBag defaulterTracingExtracts)
    {
        DefaulterTracingExtracts = defaulterTracingExtracts;
    }
}
public class MergeDefaulterTracingCommandHandler : IRequestHandler<MergeDefaulterTracingCommand, Result>
{
    private readonly IStageDefaulterTracingExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeDefaulterTracingCommandHandler(IStageDefaulterTracingExtractRepository defaulterTracingRepository, IMapper mapper)
    {
        _stageRepository = defaulterTracingRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeDefaulterTracingCommand request, CancellationToken cancellationToken)
    {
        //await _defaulterTracingRepository.MergeAsync(request.DefaulterTracingExtracts);
        var extracts = _mapper.Map<List<StageDefaulterTracingExtract>>(request.DefaulterTracingExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageDefaulterTracingExtract, DefaulterTracingSourceBag> standardizer = new(extracts, request.DefaulterTracingExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.DefaulterTracingExtracts.ManifestId.Value);

        return Result.Success();

    }
}
