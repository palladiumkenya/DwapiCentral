using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs;
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

public class MergeEnhancedAdheranceCommand : IRequest<Result>
{

    public EnhancedAdherenceCounsellingSourceBag EnhancedAdherenceCounsellingExtracts { get; set; }

    public MergeEnhancedAdheranceCommand(EnhancedAdherenceCounsellingSourceBag enhancedAdherenceCounsellingExtracts)
    {
        EnhancedAdherenceCounsellingExtracts = enhancedAdherenceCounsellingExtracts;
    }

}

public class MergeEnhancedAdheranceCommandCommandHandler : IRequestHandler<MergeEnhancedAdheranceCommand, Result>
{
    private readonly IStageEnhancedAdherenceCounsellingExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeEnhancedAdheranceCommandCommandHandler(IStageEnhancedAdherenceCounsellingExtractRepository enhancedAdherenceCounsellingRepository, IMapper mapper)
    {
        _stageRepository = enhancedAdherenceCounsellingRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeEnhancedAdheranceCommand request, CancellationToken cancellationToken)
    {
        // await _enhancedAdheranceRepository.MergeAsync(request.EnhancedAdherenceCounsellingExtracts);
        var extracts = _mapper.Map<List<StageEnhancedAdherenceCounsellingExtract>>(request.EnhancedAdherenceCounsellingExtracts);
        if (extracts.Any())
        {
            StandardizeClass<StageEnhancedAdherenceCounsellingExtract, EnhancedAdherenceCounsellingSourceBag> standardizer = new(extracts, request.EnhancedAdherenceCounsellingExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.EnhancedAdherenceCounsellingExtracts.ManifestId.Value);

        return Result.Success();

    }
}


