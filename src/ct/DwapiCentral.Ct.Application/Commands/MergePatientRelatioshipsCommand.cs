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

public class MergePatientRelatioshipsCommand : IRequest<Result>
{
    public RelationshipsSourceBag RelationshipsExtracts { get; set; }

    public MergePatientRelatioshipsCommand(RelationshipsSourceBag patientRelationshipsExtract)
    {
        RelationshipsExtracts = patientRelationshipsExtract;
    }

}

public class MergePatientRelatioshipsCommandHandler : IRequestHandler<MergePatientRelatioshipsCommand, Result>
{
    private readonly IStageRelationshipExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergePatientRelatioshipsCommandHandler(IStageRelationshipExtractRepository patientRelatioshipRepository, IMapper mapper)
    {
        _stageRepository = patientRelatioshipRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePatientRelatioshipsCommand request, CancellationToken cancellationToken)
    {
        var extracts = _mapper.Map<List<StageRelationshipsExtract>>(request.RelationshipsExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageRelationshipsExtract, RelationshipsSourceBag> standardizer = new(extracts, request.RelationshipsExtracts);
            standardizer.StandardizeExtracts();

        }

        Parallel.ForEach(extracts, extract =>
        {
            var concatenatedData = $"{extract.PatientPk}{extract.SiteCode}{extract.RelationshipToPatient}";
            var checksumHash = VisitsHash.ComputeChecksumHash(concatenatedData);
            extract.Mhash = checksumHash;
        });

        //stage
        await _stageRepository.SyncStage(extracts, request.RelationshipsExtracts.ManifestId.Value);

        return Result.Success();

    }

}
