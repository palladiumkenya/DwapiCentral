using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Application.Hashing;
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

public class MergeCovidExtractsCommand : IRequest<Result>
{
    public CovidSourceBag CovidExtracts { get; set; }

    public MergeCovidExtractsCommand(CovidSourceBag covidExtracts)
    {
        CovidExtracts = covidExtracts;
    }

}

public class MergeCovidExtractsCommandHandler : IRequestHandler<MergeCovidExtractsCommand, Result>
{
    private readonly IStageCovidExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeCovidExtractsCommandHandler(IStageCovidExtractRepository covidRepository, IMapper mapper)
    {
        _stageRepository = covidRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeCovidExtractsCommand request, CancellationToken cancellationToken)
    {
        //await _covidRepository.MergeAsync(request.CovidExtracts);
        var extracts = _mapper.Map<List<StageCovidExtract>>(request.CovidExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageCovidExtract, CovidSourceBag> standardizer = new(extracts, request.CovidExtracts);
            standardizer.StandardizeExtracts();

        }

        Parallel.ForEach(extracts, extract =>
        {
            var concatenatedData = $"{extract.PatientPk}{extract.SiteCode}{extract.VisitID}{extract.Covid19AssessmentDate}";
            var checksumHash = VisitsHash.ComputeChecksumHash(concatenatedData);
            extract.Mhash = checksumHash;
        });

        //stage
        await _stageRepository.SyncStage(extracts, request.CovidExtracts.ManifestId.Value);

        return Result.Success();

    }

}
