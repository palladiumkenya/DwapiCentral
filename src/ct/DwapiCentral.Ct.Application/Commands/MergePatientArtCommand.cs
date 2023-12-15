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

public class MergePatientArtCommand : IRequest<Result>
{
    public PatientArtSourceBag PatientArtSourceBag { get; set; }

    public MergePatientArtCommand(PatientArtSourceBag patientArtSourceBag)
    {
        PatientArtSourceBag = patientArtSourceBag;
    }
}

public class MergePatientArtCommandHandler : IRequestHandler<MergePatientArtCommand, Result>
{

    private readonly IStageArtExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergePatientArtCommandHandler(IStageArtExtractRepository patientArtExtractRepositorycs, IMapper mapper)
    {
        _stageRepository = patientArtExtractRepositorycs;
        _mapper= mapper;    
    }

    public async Task<Result> Handle(MergePatientArtCommand request, CancellationToken cancellationToken)
    {
       // await _patientArtExtractRepositorycs.MergPatientArt(request.PatientArtExtracts);
        var extracts = _mapper.Map<List<StageArtExtract>>(request.PatientArtSourceBag.Extracts);



            if (extracts.Any())
            {
                StandardizeClass<StageArtExtract, PatientArtSourceBag> standardizer = new(extracts, request.PatientArtSourceBag);
                standardizer.StandardizeExtracts();

            }

        //stage
        await _stageRepository.SyncStage(extracts, request.PatientArtSourceBag.ManifestId.Value);

        return Result.Success();
    }
}
