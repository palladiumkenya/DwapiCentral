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

public class MergeIITRiskScoreCommand : IRequest<Result>
{
    public IITRiskScoreSourceBag IITRiskScoreExtracts { get; set; }

    public MergeIITRiskScoreCommand(IITRiskScoreSourceBag IITRiskScoreExtract)
    {
        IITRiskScoreExtracts = IITRiskScoreExtract;
    }
}
public class MergeIITRiskScoreCommanddHandler : IRequestHandler<MergeIITRiskScoreCommand, Result>
{
    private readonly IStageIITRiskScoreRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeIITRiskScoreCommanddHandler(IStageIITRiskScoreRepository iitRiskScoreRepository, IMapper mapper)
    {
        _stageRepository = iitRiskScoreRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeIITRiskScoreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var extracts = _mapper.Map<List<StageIITRiskScore>>(request.IITRiskScoreExtracts.Extracts);
            if (extracts.Any())
            {
                StandardizeClass<StageIITRiskScore, IITRiskScoreSourceBag> standardizer = new(extracts, request.IITRiskScoreExtracts);
                standardizer.StandardizeExtracts();

            }


            Parallel.ForEach(extracts, extract =>
            {
                var concatenatedData = $"{extract.PatientPk}{extract.SiteCode}";
                var checksumHash = VisitsHash.ComputeChecksumHash(concatenatedData);
                extract.Mhash = checksumHash;
            });

            //stage
            await _stageRepository.SyncStage(extracts, request.IITRiskScoreExtracts.ManifestId.Value);

            return Result.Success();

        }catch(Exception ex)
        {
            return Result.Failure(ex.Message);
        }

    }
}
