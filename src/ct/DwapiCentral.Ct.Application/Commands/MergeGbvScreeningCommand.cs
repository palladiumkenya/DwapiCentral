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

public class MergeGbvScreeningCommand : IRequest<Result>
{
    public IEnumerable<GbvScreeningExtract> GbvScreeningExtracts { get; set; }

    public MergeGbvScreeningCommand(IEnumerable<GbvScreeningExtract> gbvScreeningExtracts)
    {
        GbvScreeningExtracts = gbvScreeningExtracts;
    }

}

public class MergeGbvScreeningCommandHandler : IRequestHandler<MergeGbvScreeningCommand, Result>
{
    private readonly IGbvScreeningRepository _gbvScreeningRepository;

    public MergeGbvScreeningCommandHandler(IGbvScreeningRepository gbvScreeningRepository)
    {
        _gbvScreeningRepository = gbvScreeningRepository;
    }

    public async Task<Result> Handle(MergeGbvScreeningCommand request, CancellationToken cancellationToken)
    {

        await _gbvScreeningRepository.MergeAsync(request.GbvScreeningExtracts);

        return Result.Success();

    }

}
