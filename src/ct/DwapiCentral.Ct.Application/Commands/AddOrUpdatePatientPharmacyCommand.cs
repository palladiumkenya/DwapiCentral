using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs;
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

public class AddOrUpdatePatientPharmacyCommand : IRequest<Result>
{
    public PharmacySourceBag PatientPharmacy { get; set; } 

    public AddOrUpdatePatientPharmacyCommand(PharmacySourceBag patientPharmacy)
    {
        PatientPharmacy= patientPharmacy;
    }
}

public class AddOrUpdatePatientPharmacyCommandHandler : IRequestHandler<AddOrUpdatePatientPharmacyCommand, Result>
{
    private readonly IStagePharmacyExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public AddOrUpdatePatientPharmacyCommandHandler(IStagePharmacyExtractRepository stagePharmacyRepository, IMapper mapper)
    {
        _stageRepository = stagePharmacyRepository;
        _mapper = mapper;   
    }

    public async Task<Result> Handle(AddOrUpdatePatientPharmacyCommand request, CancellationToken cancellationToken)
    {
        // await _patientPharmacyRepository.MergePharmacyExtractsAsync(request.PatientPharmacy);
        var extracts = _mapper.Map<List<StagePharmacyExtract>>(request.PatientPharmacy.Extracts);



        if (extracts.Any())
        {
            StandardizeClass<StagePharmacyExtract, PharmacySourceBag> standardizer = new(extracts, request.PatientPharmacy);
            standardizer.StandardizeExtracts();

        }

        //stage
        await _stageRepository.SyncStage(extracts, request.PatientPharmacy.ManifestId.Value);

        return Result.Success();
    }
}
