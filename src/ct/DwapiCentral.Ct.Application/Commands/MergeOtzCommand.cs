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

public class MergeOtzCommand : IRequest<Result>
{

    public IEnumerable<OtzExtract> OtzExtracts { get; set; }

    public MergeOtzCommand(IEnumerable<OtzExtract> otzExtracts)
    {
        OtzExtracts = otzExtracts;
    }

}

public class MergeOtzCommandHandler : IRequestHandler<MergeOtzCommand, Result>
{
    private readonly IOtzRepository _otzRepository;

    public MergeOtzCommandHandler(IOtzRepository otzRepository)
    {
        _otzRepository = otzRepository;
    }

    public async Task<Result> Handle(MergeOtzCommand request, CancellationToken cancellationToken)
    {

        await _otzRepository.MergeAsync(request.OtzExtracts);

        return Result.Success();

    }

}


