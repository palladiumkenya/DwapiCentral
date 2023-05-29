using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands;

public class MergeContactListingCommand : IRequest<Result>
{
    public IEnumerable<ContactListingExtract> ContactListingExtracts { get; set; }

    public MergeContactListingCommand(IEnumerable<ContactListingExtract> contactListingExtracts)
    {
        ContactListingExtracts = contactListingExtracts;
    }
}
public class MergeContactListingCommandHandler : IRequestHandler<MergeContactListingCommand, Result>
{
    private readonly IContactListingRepository _contactListingRepository;

    public MergeContactListingCommandHandler(IContactListingRepository contactListingRepository)
    {
        _contactListingRepository = contactListingRepository;
    }

    public async Task<Result> Handle(MergeContactListingCommand request, CancellationToken cancellationToken)
    {

        await _contactListingRepository.MergeAsync(request.ContactListingExtracts);

        return Result.Success();

    }
}
