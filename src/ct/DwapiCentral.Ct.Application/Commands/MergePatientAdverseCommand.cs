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

public class MergePatientAdverseCommand : IRequest<Result>
{
    public AdverseEventSourceBag PatientAdverseEventExtracts { get; set; }

    public MergePatientAdverseCommand(AdverseEventSourceBag patientAdverseEventExtracts)
    {
        PatientAdverseEventExtracts = patientAdverseEventExtracts;
    }

}

public class MergePatientAdverseCommandHandler : IRequestHandler<MergePatientAdverseCommand, Result>
{
    private readonly IStageAdverseEventExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergePatientAdverseCommandHandler(IStageAdverseEventExtractRepository patientAdverseEventRepository, IMapper mapper)
    {
        _stageRepository = patientAdverseEventRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePatientAdverseCommand request, CancellationToken cancellationToken)
    {
        //await _patientAdverseRepository.MergeAsync(request.PatientAdverseEventExtracts);
        var extracts = _mapper.Map<List<StageAdverseEventExtract>>(request.PatientAdverseEventExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageAdverseEventExtract, AdverseEventSourceBag> standardizer = new(extracts, request.PatientAdverseEventExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.PatientAdverseEventExtracts.ManifestId.Value);

        return Result.Success();

    }

}

