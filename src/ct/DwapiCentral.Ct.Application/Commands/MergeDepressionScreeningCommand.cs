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

public class MergeDepressionScreeningCommand : IRequest<Result>
{
    public DepressionScreeningSourceBag DepressionScreeningExtracts { get; set; }

    public MergeDepressionScreeningCommand(DepressionScreeningSourceBag depressionScreeningExtracts)
    {
        DepressionScreeningExtracts = depressionScreeningExtracts;
    }
}
public class MergeDepressionScreeningCommandHandler : IRequestHandler<MergeDepressionScreeningCommand, Result>
{
    private readonly IStageDepressionScreeningExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeDepressionScreeningCommandHandler(IStageDepressionScreeningExtractRepository depressionScreeningRepository, IMapper mapper)
    {
        _stageRepository = depressionScreeningRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeDepressionScreeningCommand request, CancellationToken cancellationToken)
    {
        // await _depressionScreeningRepository.MergeAsync(request.DepressionScreeningExtracts);
        var extracts = _mapper.Map<List<StageDepressionScreeningExtract>>(request.DepressionScreeningExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageDepressionScreeningExtract, DepressionScreeningSourceBag> standardizer = new(extracts, request.DepressionScreeningExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.DepressionScreeningExtracts.ManifestId.Value);

        return Result.Success();

    }
}

