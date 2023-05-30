using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Interfaces.Repository.Base;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands;

public class MergePatientVisitCommand : IRequest<Result>
{
    public IEnumerable<PatientVisitExtract> PatientVisits;

    public MergePatientVisitCommand(IEnumerable<PatientVisitExtract> patientVisits)
    {
        PatientVisits = patientVisits;
    }

    
}

public class MergePatientVisitCommandHandler : IRequestHandler<MergePatientVisitCommand, Result>
{
    private readonly IPatientVisitExtractRepository _patientVisitRepository;

    public MergePatientVisitCommandHandler(IPatientVisitExtractRepository patientVisitExtractRepository)
    {
        _patientVisitRepository= patientVisitExtractRepository;
    }

    public async Task<Result> Handle(MergePatientVisitCommand request, CancellationToken cancellationToken)
    {
        

        await _patientVisitRepository.MergeAsync(request.PatientVisits);

            
        
        return Result.Success();

    }
}
