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

public class MergeDepressionScreeningCommand : IRequest<Result>
{
    public IEnumerable<DepressionScreeningExtract> DepressionScreeningExtracts { get; set; }

    public MergeDepressionScreeningCommand(IEnumerable<DepressionScreeningExtract> depressionScreeningExtracts)
    {
        DepressionScreeningExtracts = depressionScreeningExtracts;
    }
}
public class MergeDepressionScreeningCommandHandler : IRequestHandler<MergeDepressionScreeningCommand, Result>
{
    private readonly IDepressionScreeningRepository _depressionScreeningRepository;

    public MergeDepressionScreeningCommandHandler(IDepressionScreeningRepository depressionScreeningRepository)
    {
        _depressionScreeningRepository = depressionScreeningRepository;
    }

    public async Task<Result> Handle(MergeDepressionScreeningCommand request, CancellationToken cancellationToken)
    {

        await _depressionScreeningRepository.MergeAsync(request.DepressionScreeningExtracts);

        return Result.Success();

    }
}

