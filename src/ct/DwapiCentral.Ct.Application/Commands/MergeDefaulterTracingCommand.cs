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

public class MergeDefaulterTracingCommand : IRequest<Result>
{
    public IEnumerable<DefaulterTracingExtract> DefaulterTracingExtracts { get; set; }

    public MergeDefaulterTracingCommand(IEnumerable<DefaulterTracingExtract> defaulterTracingExtracts)
    {
        DefaulterTracingExtracts = defaulterTracingExtracts;
    }
}
public class MergeDefaulterTracingCommandHandler : IRequestHandler<MergeDefaulterTracingCommand, Result>
{
    private readonly IDefaulterTracingRepository _defaulterTracingRepository;

    public MergeDefaulterTracingCommandHandler(IDefaulterTracingRepository defaulterTracingRepository)
    {
        _defaulterTracingRepository = defaulterTracingRepository;
    }

    public async Task<Result> Handle(MergeDefaulterTracingCommand request, CancellationToken cancellationToken)
    {

        await _defaulterTracingRepository.MergeAsync(request.DefaulterTracingExtracts);

        return Result.Success();

    }
}
