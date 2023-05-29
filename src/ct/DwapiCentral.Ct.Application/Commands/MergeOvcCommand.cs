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

public class MergeOvcCommand : IRequest<Result>
{
    public IEnumerable<OvcExtract> OvcExtracts { get; set; }

    public MergeOvcCommand(IEnumerable<OvcExtract> ovcExtracts)
    {
        OvcExtracts = ovcExtracts;
    }

}

public class MergeOvcCommandHandler : IRequestHandler<MergeOvcCommand, Result>
{
    private readonly IOvcRepository _ovcRepository;

    public MergeOvcCommandHandler(IOvcRepository ovcRepository)
    {
        _ovcRepository = ovcRepository;
    }

    public async Task<Result> Handle(MergeOvcCommand request, CancellationToken cancellationToken)
    {

        await _ovcRepository.MergeAsync(request.OvcExtracts);

        return Result.Success();

    }

}

