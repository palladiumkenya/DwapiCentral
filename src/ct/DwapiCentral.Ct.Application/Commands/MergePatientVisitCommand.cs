using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Application.Hashing;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
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

public class MergePatientVisitCommand : IRequest<Result>
{
    public PatientVisitSourceBag PatientVisits;

    public MergePatientVisitCommand(PatientVisitSourceBag patientVisits)
    {
        PatientVisits = patientVisits;
    }

    
}

public class MergePatientVisitCommandHandler : IRequestHandler<MergePatientVisitCommand, Result>
{
    private readonly IStageVisitExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergePatientVisitCommandHandler(IStageVisitExtractRepository patientVisitExtractRepository, IMapper mapper)
    {
        _stageRepository = patientVisitExtractRepository;
        _mapper= mapper;
    }

    public async Task<Result> Handle(MergePatientVisitCommand request, CancellationToken cancellationToken)
    {
        //await _patientVisitRepository.MergeAsync(request.PatientVisits);
        var extracts = _mapper.Map<List<StageVisitExtract>>(request.PatientVisits.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageVisitExtract, PatientVisitSourceBag> standardizer = new(extracts, request.PatientVisits);
            standardizer.StandardizeExtracts();

        }

        Parallel.ForEach(extracts, extract =>
        {
            var concatenatedData = $"{extract.PatientPk}{extract.SiteCode}{extract.VisitId}{extract.VisitDate}";
            var checksumHash = VisitsHash.ComputeChecksumHash(concatenatedData);
            extract.Mhash = checksumHash;
        });


        await _stageRepository.SyncStage(extracts, request.PatientVisits.ManifestId.Value);



        return Result.Success();

    }

    
    
}
