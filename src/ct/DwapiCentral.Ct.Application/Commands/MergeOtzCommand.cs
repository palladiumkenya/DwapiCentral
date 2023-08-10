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

public class MergeOtzCommand : IRequest<Result>
{

    public OtzSourceBag OtzExtracts { get; set; }

    public MergeOtzCommand(OtzSourceBag otzExtracts)
    {
        OtzExtracts = otzExtracts;
    }

}

public class MergeOtzCommandHandler : IRequestHandler<MergeOtzCommand, Result>
{
    private readonly IStageOtzExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeOtzCommandHandler(IStageOtzExtractRepository otzRepository, IMapper mapper)
    {
        _stageRepository = otzRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeOtzCommand request, CancellationToken cancellationToken)
    {
        //await _otzRepository.MergeAsync(request.OtzExtracts);
        var extracts = _mapper.Map<List<StageOtzExtract>>(request.OtzExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageOtzExtract, OtzSourceBag> standardizer = new(extracts, request.OtzExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.OtzExtracts.ManifestId.Value);

        return Result.Success();

    }

}


