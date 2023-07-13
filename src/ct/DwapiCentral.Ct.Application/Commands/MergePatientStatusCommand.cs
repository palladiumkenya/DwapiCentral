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

public class MergePatientStatusCommand : IRequest<Result>
{
    public StatusSourceBag PatientStatusExtracts { get; set; }

    public MergePatientStatusCommand(StatusSourceBag patientStatusExtracts)
    {
        PatientStatusExtracts = patientStatusExtracts;
    }

}

public class MergePatientStatusCommandHandler : IRequestHandler<MergePatientStatusCommand, Result>
{
    private readonly IStageStatusExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergePatientStatusCommandHandler(IStageStatusExtractRepository patientStatusRepository, IMapper mapper)
    {
        _stageRepository = patientStatusRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePatientStatusCommand request, CancellationToken cancellationToken)
    {
        // await _patientStatusRepository.MergeAsync(request.PatientStatusExtracts);

        var extracts = _mapper.Map<List<StageStatusExtract>>(request.PatientStatusExtracts);
        if (extracts.Any())
        {
            StandardizeClass<StageStatusExtract, StatusSourceBag> standardizer = new(extracts, request.PatientStatusExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.PatientStatusExtracts.ManifestId.Value);

        return Result.Success();

    }

}
