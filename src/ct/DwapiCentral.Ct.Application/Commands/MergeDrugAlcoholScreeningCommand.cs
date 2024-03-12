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

public class MergeDrugAlcoholScreeningCommand : IRequest<Result>
{
    public DrugAlcoholScreeningSourceBag DrugAlcoholScreeningExtracts { get; set; }

    public MergeDrugAlcoholScreeningCommand(DrugAlcoholScreeningSourceBag drugAlcoholScreeningExtracts)
    {
        DrugAlcoholScreeningExtracts = drugAlcoholScreeningExtracts;
    }
}
public class MergeDrugAlcoholScreeningCommandHandler : IRequestHandler<MergeDrugAlcoholScreeningCommand, Result>
{
    private readonly IStageDrugAlcoholScreeningExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeDrugAlcoholScreeningCommandHandler(IStageDrugAlcoholScreeningExtractRepository drugAlcoholScreeningRepository, IMapper mapper)
    {
        _stageRepository = drugAlcoholScreeningRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeDrugAlcoholScreeningCommand request, CancellationToken cancellationToken)
    {
        //await _drugAlcoholScreeningRepository.MergeAsync(request.DrugAlcoholScreeningExtracts);
        var extracts = _mapper.Map<List<StageDrugAlcoholScreeningExtract>>(request.DrugAlcoholScreeningExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageDrugAlcoholScreeningExtract, DrugAlcoholScreeningSourceBag> standardizer = new(extracts, request.DrugAlcoholScreeningExtracts);
            standardizer.StandardizeExtracts();

        }

        Parallel.ForEach(extracts, extract =>
        {
            var concatenatedData = $"{extract.PatientPk}{extract.SiteCode}{extract.VisitID}{extract.VisitDate}";
            var checksumHash = VisitsHash.ComputeChecksumHash(concatenatedData);
            extract.Mhash = checksumHash;
        });

        //stage
        await _stageRepository.SyncStage(extracts, request.DrugAlcoholScreeningExtracts.ManifestId.Value);

        return Result.Success();

    }
}
