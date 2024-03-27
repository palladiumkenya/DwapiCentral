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

public class MergeAllergiesChronicIllnessCommand : IRequest<Result>
{

    public AllergiesChronicIllnessSourceBag AllergiesChronicIllnessExtracts { get; set; }

    public MergeAllergiesChronicIllnessCommand(AllergiesChronicIllnessSourceBag allergiesChronicIllnessExtracts)
    {
        AllergiesChronicIllnessExtracts = allergiesChronicIllnessExtracts;
    }

}

public class MergeAllergiesChronicIllnessCommandHandler : IRequestHandler<MergeAllergiesChronicIllnessCommand, Result>
{
    private readonly IStageAllergiesChronicIllnessExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeAllergiesChronicIllnessCommandHandler(IStageAllergiesChronicIllnessExtractRepository stageAllergiesChronicIllnessRepository, IMapper mapper)
    {
        _stageRepository = stageAllergiesChronicIllnessRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeAllergiesChronicIllnessCommand request, CancellationToken cancellationToken)
    {

        try
        {
            //await _allergiesChronicIllnessRepository.MergeAsync(request.AllergiesChronicIllnessExtracts);
            var extracts = _mapper.Map<List<StageAllergiesChronicIllnessExtract>>(request.AllergiesChronicIllnessExtracts.Extracts);
            if (extracts.Any())
            {
                StandardizeClass<StageAllergiesChronicIllnessExtract, AllergiesChronicIllnessSourceBag> standardizer = new(extracts, request.AllergiesChronicIllnessExtracts);
                standardizer.StandardizeExtracts();

            }

            Parallel.ForEach(extracts, extract =>
            {
                var concatenatedData = $"{extract.PatientPk}{extract.SiteCode}{extract.VisitID}{extract.VisitDate}";
                var checksumHash = VisitsHash.ComputeChecksumHash(concatenatedData);
                extract.Mhash = checksumHash;
            });


            //stage
            await _stageRepository.SyncStage(extracts, request.AllergiesChronicIllnessExtracts.ManifestId.Value);
            return Result.Success();

        }
        catch(Exception ex)
        {
            return Result.Failure(ex.Message);
        }

    }
}

