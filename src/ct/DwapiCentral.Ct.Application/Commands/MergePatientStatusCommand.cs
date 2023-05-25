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

public class MergePatientStatusCommand : IRequest<Result>
{
    public IEnumerable<PatientStatusExtract> PatientStatusExtracts { get; set; }

    public MergePatientStatusCommand(IEnumerable<PatientStatusExtract> patientStatusExtracts)
    {
        PatientStatusExtracts = patientStatusExtracts;
    }

}

public class MergePatientStatusCommandHandler : IRequestHandler<MergePatientStatusCommand, Result>
{
    private readonly IPatientStatusRepository _patientStatusRepository;

    public MergePatientStatusCommandHandler(IPatientStatusRepository patientStatusRepository)
    {
        _patientStatusRepository = patientStatusRepository;
    }

    public async Task<Result> Handle(MergePatientStatusCommand request, CancellationToken cancellationToken)
    {

        await _patientStatusRepository.MergeAsync(request.PatientStatusExtracts);

        return Result.Success();

    }

}
