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

public class MergeContactListingCommand : IRequest<Result>
{
    public ContactListingSourceBag ContactListingExtracts { get; set; }

    public MergeContactListingCommand(ContactListingSourceBag contactListingExtracts)
    {
        ContactListingExtracts = contactListingExtracts;
    }
}
public class MergeContactListingCommandHandler : IRequestHandler<MergeContactListingCommand, Result>
{
    private readonly IStageContactListingExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeContactListingCommandHandler(IStageContactListingExtractRepository stageContactListingRepository, IMapper mapper)
    {
        _stageRepository = stageContactListingRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeContactListingCommand request, CancellationToken cancellationToken)
    {
        //await _contactListingRepository.MergeAsync(request.ContactListingExtracts);
        var extracts = _mapper.Map<List<StageContactListingExtract>>(request.ContactListingExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageContactListingExtract, ContactListingSourceBag> standardizer = new(extracts, request.ContactListingExtracts);
            standardizer.StandardizeExtracts();
        }

        Parallel.ForEach(extracts, extract =>
        {
            var concatenatedData = $"{extract.PatientPk}{extract.SiteCode}{extract.ContactAge}{extract.RelationshipWithPatient}";
            var checksumHash = VisitsHash.ComputeChecksumHash(concatenatedData);
            extract.Mhash = checksumHash;
        });

        //stage
        await _stageRepository.SyncStage(extracts, request.ContactListingExtracts.ManifestId.Value);

        return Result.Success();

    }
}
