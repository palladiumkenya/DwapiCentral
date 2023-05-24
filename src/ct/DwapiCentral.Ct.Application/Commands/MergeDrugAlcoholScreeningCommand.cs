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

public class MergeDrugAlcoholScreeningCommand : IRequest<Result>
{
    public IEnumerable<DrugAlcoholScreeningExtract> DrugAlcoholScreeningExtracts { get; set; }

    public MergeDrugAlcoholScreeningCommand(IEnumerable<DrugAlcoholScreeningExtract> drugAlcoholScreeningExtracts)
    {
        DrugAlcoholScreeningExtracts = drugAlcoholScreeningExtracts;
    }
}
public class MergeDrugAlcoholScreeningCommandHandler : IRequestHandler<MergeDrugAlcoholScreeningCommand, Result>
{
    private readonly IDrugAlcoholScreeningRepository _drugAlcoholScreeningRepository;

    public MergeDrugAlcoholScreeningCommandHandler(IDrugAlcoholScreeningRepository drugAlcoholScreeningRepository)
    {
        _drugAlcoholScreeningRepository = drugAlcoholScreeningRepository;
    }

    public async Task<Result> Handle(MergeDrugAlcoholScreeningCommand request, CancellationToken cancellationToken)
    {

        await _drugAlcoholScreeningRepository.MergeAsync(request.DrugAlcoholScreeningExtracts);

        return Result.Success();

    }
}
