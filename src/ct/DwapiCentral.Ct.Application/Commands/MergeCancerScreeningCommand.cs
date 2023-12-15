using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands;

public class MergeCancerScreeningCommand : IRequest<Result>
{
    public CancerScreeningSourceBag cancerScreeningSource { get; set; }

    public MergeCancerScreeningCommand(CancerScreeningSourceBag _cancerScreeningSource)
    {
        cancerScreeningSource = _cancerScreeningSource;
    }

}

public class MergeCancerScreeningCommandHandler : IRequestHandler<MergeCancerScreeningCommand, Result>
{
    private readonly IStageCancerScreeningExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeCancerScreeningCommandHandler(IStageCancerScreeningExtractRepository cancerScreeningRepository, IMapper mapper)
    {
        _stageRepository = cancerScreeningRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeCancerScreeningCommand request, CancellationToken cancellationToken)
    {
        var extracts = _mapper.Map<List<StageCancerScreeningExtract>>(request.cancerScreeningSource.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageCancerScreeningExtract, CancerScreeningSourceBag> standardizer = new(extracts, request.cancerScreeningSource);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.cancerScreeningSource.ManifestId.Value);

        return Result.Success();

    }

}
