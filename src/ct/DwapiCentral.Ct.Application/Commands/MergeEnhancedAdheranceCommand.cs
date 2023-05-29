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

public class MergeEnhancedAdheranceCommand : IRequest<Result>
{

    public IEnumerable<EnhancedAdherenceCounsellingExtract> EnhancedAdherenceCounsellingExtracts { get; set; }

    public MergeEnhancedAdheranceCommand(IEnumerable<EnhancedAdherenceCounsellingExtract> enhancedAdherenceCounsellingExtracts)
    {
        EnhancedAdherenceCounsellingExtracts = enhancedAdherenceCounsellingExtracts;
    }

}

public class MergeEnhancedAdheranceCommandCommandHandler : IRequestHandler<MergeEnhancedAdheranceCommand, Result>
{
    private readonly IEnhancedAdherenceCounsellingRepository _enhancedAdheranceRepository;

    public MergeEnhancedAdheranceCommandCommandHandler(IEnhancedAdherenceCounsellingRepository enhancedAdherenceCounsellingRepository)
    {
        _enhancedAdheranceRepository = enhancedAdherenceCounsellingRepository;
    }

    public async Task<Result> Handle(MergeEnhancedAdheranceCommand request, CancellationToken cancellationToken)
    {

        await _enhancedAdheranceRepository.MergeAsync(request.EnhancedAdherenceCounsellingExtracts);

        return Result.Success();

    }
}


