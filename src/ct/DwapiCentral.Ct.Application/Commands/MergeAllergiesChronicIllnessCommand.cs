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

public class MergeAllergiesChronicIllnessCommand : IRequest<Result>
{

    public IEnumerable<AllergiesChronicIllnessExtract> AllergiesChronicIllnessExtracts { get; set; }

    public MergeAllergiesChronicIllnessCommand(IEnumerable<AllergiesChronicIllnessExtract> allergiesChronicIllnessExtracts)
    {
        AllergiesChronicIllnessExtracts = allergiesChronicIllnessExtracts;
    }

}

public class MergeAllergiesChronicIllnessCommandHandler : IRequestHandler<MergeAllergiesChronicIllnessCommand, Result>
{
    private readonly IAllergiesChronicIllnessRepository _allergiesChronicIllnessRepository;

    public MergeAllergiesChronicIllnessCommandHandler(IAllergiesChronicIllnessRepository allergiesChronicIllnessRepository)
    {
        _allergiesChronicIllnessRepository = allergiesChronicIllnessRepository;
    }

    public async Task<Result> Handle(MergeAllergiesChronicIllnessCommand request, CancellationToken cancellationToken)
    {

        await _allergiesChronicIllnessRepository.MergeAsync(request.AllergiesChronicIllnessExtracts);

        return Result.Success();

    }
}

