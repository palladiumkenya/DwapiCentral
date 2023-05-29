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

public class MergeCovidExtractsCommand : IRequest<Result>
{
    public IEnumerable<CovidExtract> CovidExtracts { get; set; }

    public MergeCovidExtractsCommand(IEnumerable<CovidExtract> covidExtracts)
    {
        CovidExtracts = covidExtracts;
    }

}

public class MergeCovidExtractsCommandHandler : IRequestHandler<MergeCovidExtractsCommand, Result>
{
    private readonly ICovidRepository _covidRepository;

    public MergeCovidExtractsCommandHandler(ICovidRepository covidRepository)
    {
        _covidRepository = covidRepository;
    }

    public async Task<Result> Handle(MergeCovidExtractsCommand request, CancellationToken cancellationToken)
    {

        await _covidRepository.MergeAsync(request.CovidExtracts);

        return Result.Success();

    }

}
