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

public class MergePatientBaselinesCommand : IRequest<Result>
{
    public PatientBaselineSourceBag PatientBaselinesExtracts { get; set; }

    public MergePatientBaselinesCommand(PatientBaselineSourceBag patientBaselinesExtracts)
    {
        PatientBaselinesExtracts = patientBaselinesExtracts;
    }

}

public class MergePatientBaselinesCommandHandler : IRequestHandler<MergePatientBaselinesCommand, Result>
{
    private readonly IStageBaselineExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergePatientBaselinesCommandHandler(IStageBaselineExtractRepository patientBaseLinesRepository, IMapper mapper)
    {
        _stageRepository = patientBaseLinesRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePatientBaselinesCommand request, CancellationToken cancellationToken)
    {
        //await _patientBaselineRepository.MergeAsync(request.PatientBaselinesExtracts);
        var extracts = _mapper.Map<List<StageBaselineExtract>>(request.PatientBaselinesExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageBaselineExtract, PatientBaselineSourceBag> standardizer = new(extracts, request.PatientBaselinesExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.PatientBaselinesExtracts.ManifestId.Value);

        return Result.Success();

    }

}

