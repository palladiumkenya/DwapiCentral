using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Application.Hashing;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands;

public class MergeCervicalCancerScreeningCommand : IRequest<Result>
{
    public CervicalCancerScreeningSourceBag cervicalCancerScreeningSource { get; set; }

    public MergeCervicalCancerScreeningCommand(CervicalCancerScreeningSourceBag _cervicalCancerScreeningSource)
    {
        cervicalCancerScreeningSource = _cervicalCancerScreeningSource;
    }

}

public class MergeCervicalCancerScreeningCommandHandler : IRequestHandler<MergeCervicalCancerScreeningCommand, Result>
{
    private readonly IStageCervicalCancerScreeningExtractsRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeCervicalCancerScreeningCommandHandler(IStageCervicalCancerScreeningExtractsRepository cervicalCancerScreeningRepository, IMapper mapper)
    {
        _stageRepository = cervicalCancerScreeningRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeCervicalCancerScreeningCommand request, CancellationToken cancellationToken)
    {
        var extracts = _mapper.Map<List<StageCervicalCancerScreeningExtract>>(request.cervicalCancerScreeningSource.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageCervicalCancerScreeningExtract, CervicalCancerScreeningSourceBag> standardizer = new(extracts, request.cervicalCancerScreeningSource);
            standardizer.StandardizeExtracts();

        }

        Parallel.ForEach(extracts, extract =>
        {
            var concatenatedData = $"{extract.PatientPk}{extract.SiteCode}{extract.VisitID}{extract.VisitDate}";
            var checksumHash = VisitsHash.ComputeChecksumHash(concatenatedData);
            extract.Mhash = checksumHash;
        });

        //stage
        await _stageRepository.SyncStage(extracts, request.cervicalCancerScreeningSource.ManifestId.Value);

        return Result.Success();

    }

}
