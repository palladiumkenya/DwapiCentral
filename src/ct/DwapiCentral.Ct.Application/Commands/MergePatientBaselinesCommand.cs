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

public class MergePatientBaselinesCommand : IRequest<Result>
{
    public IEnumerable<PatientBaselinesExtract> PatientBaselinesExtracts { get; set; }

    public MergePatientBaselinesCommand(IEnumerable<PatientBaselinesExtract> patientBaselinesExtracts)
    {
        PatientBaselinesExtracts = patientBaselinesExtracts;
    }

}

public class MergePatientBaselinesCommandHandler : IRequestHandler<MergePatientBaselinesCommand, Result>
{
    private readonly IPatientBaseLinesRepository _patientBaselineRepository;

    public MergePatientBaselinesCommandHandler(IPatientBaseLinesRepository patientBaseLinesRepository)
    {
        _patientBaselineRepository = patientBaseLinesRepository;
    }

    public async Task<Result> Handle(MergePatientBaselinesCommand request, CancellationToken cancellationToken)
    {

        await _patientBaselineRepository.MergeAsync(request.PatientBaselinesExtracts);

        return Result.Success();

    }

}

