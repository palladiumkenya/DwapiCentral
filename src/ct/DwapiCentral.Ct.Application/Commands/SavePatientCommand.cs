using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Shared.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands;

public class SavePatientCommand : IRequest<Result>
{
    public PatientSourceBag PatientSourceBag { get; }
    public SavePatientCommand(PatientSourceBag patientSourceBag)
    {
        PatientSourceBag = patientSourceBag;
    }
}

public class SavePatientCommandHandler : IRequestHandler<SavePatientCommand, Result>
{
    private readonly IStagePatientExtractRepository _patientExtractRepository;
    private readonly IMapper _mapper;
    

    public SavePatientCommandHandler(IStagePatientExtractRepository patientExtractRepository,IMapper mapper)
    {
        _patientExtractRepository = patientExtractRepository;
        _mapper = mapper;   
    }

    public async Task<Result> Handle(SavePatientCommand request, CancellationToken cancellationToken)
    {
        //await _patientExtractRepository.MergeAsync(request.PatientExtract);
        var extracts = _mapper.Map<List<StagePatientExtract>>(request.PatientSourceBag.Extracts);

        
        //stage
        await _patientExtractRepository.SyncStage(extracts, request.PatientSourceBag.ManifestId.Value);
     

        return Result.Success();
    }
}
