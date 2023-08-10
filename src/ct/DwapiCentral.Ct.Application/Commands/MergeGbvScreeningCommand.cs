using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Domain.Models.Extracts;
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

public class MergeGbvScreeningCommand : IRequest<Result>
{
    public GbvScreeningSourceBag GbvScreeningExtracts { get; set; }

    public MergeGbvScreeningCommand(GbvScreeningSourceBag gbvScreeningExtracts)
    {
        GbvScreeningExtracts = gbvScreeningExtracts;
    }

}

public class MergeGbvScreeningCommandHandler : IRequestHandler<MergeGbvScreeningCommand, Result>
{
    private readonly IStageGbvScreeningExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeGbvScreeningCommandHandler(IStageGbvScreeningExtractRepository gbvScreeningRepository, IMapper mapper)
    {
        _stageRepository = gbvScreeningRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeGbvScreeningCommand request, CancellationToken cancellationToken)
    {
        //await _gbvScreeningRepository.MergeAsync(request.GbvScreeningExtracts);
        var extracts = _mapper.Map<List<StageGbvScreeningExtract>>(request.GbvScreeningExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageGbvScreeningExtract, GbvScreeningSourceBag> standardizer = new(extracts, request.GbvScreeningExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.GbvScreeningExtracts.ManifestId.Value);

        return Result.Success();

    }

}
