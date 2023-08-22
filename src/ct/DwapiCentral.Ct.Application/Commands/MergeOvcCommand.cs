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

public class MergeOvcCommand : IRequest<Result>
{
    public OvcSourceBag OvcExtracts { get; set; }

    public MergeOvcCommand(OvcSourceBag ovcExtracts)
    {
        OvcExtracts = ovcExtracts;
    }

}

public class MergeOvcCommandHandler : IRequestHandler<MergeOvcCommand, Result>
{
    private readonly IStageOvcExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeOvcCommandHandler(IStageOvcExtractRepository ovcRepository, IMapper mapper)
    {
        _stageRepository = ovcRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeOvcCommand request, CancellationToken cancellationToken)
    {
        //await _ovcRepository.MergeAsync(request.OvcExtracts);
        var extracts = _mapper.Map<List<StageOvcExtract>>(request.OvcExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageOvcExtract, OvcSourceBag> standardizer = new(extracts, request.OvcExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.OvcExtracts.ManifestId.Value);

        return Result.Success();

    }

}

