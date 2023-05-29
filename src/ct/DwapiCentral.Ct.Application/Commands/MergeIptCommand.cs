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

public class MergeIptCommand : IRequest<Result>
{

    public IEnumerable<IptExtract> IptExtracts { get; set; }

    public MergeIptCommand(IEnumerable<IptExtract> iptExtracts)
    {
        IptExtracts = iptExtracts;
    }

}

public class MergeIptCommandHandler : IRequestHandler<MergeIptCommand, Result>
{
    private readonly IIptRepository _iptRepository;

    public MergeIptCommandHandler(IIptRepository iptRepository)
    {
        _iptRepository = iptRepository;
    }

    public async Task<Result> Handle(MergeIptCommand request, CancellationToken cancellationToken)
    {

        await _iptRepository.MergeAsync(request.IptExtracts);

        return Result.Success();

    }

}
