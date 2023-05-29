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

public class MergePatientAdverseCommand : IRequest<Result>
{
    public IEnumerable<PatientAdverseEventExtract> PatientAdverseEventExtracts { get; set; }

    public MergePatientAdverseCommand(IEnumerable<PatientAdverseEventExtract> patientAdverseEventExtracts)
    {
        PatientAdverseEventExtracts = patientAdverseEventExtracts;
    }

}

public class MergePatientAdverseCommandHandler : IRequestHandler<MergePatientAdverseCommand, Result>
{
    private readonly IPatientAdverseEventRepository _patientAdverseRepository;

    public MergePatientAdverseCommandHandler(IPatientAdverseEventRepository patientAdverseEventRepository)
    {
        _patientAdverseRepository = patientAdverseEventRepository;
    }

    public async Task<Result> Handle(MergePatientAdverseCommand request, CancellationToken cancellationToken)
    {

        await _patientAdverseRepository.MergeAsync(request.PatientAdverseEventExtracts);

        return Result.Success();

    }

}

