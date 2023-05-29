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

public class SavePatientCommand : IRequest<Result>
{
    public IEnumerable<PatientExtract> PatientExtract { get; set; }

    public SavePatientCommand(IEnumerable<PatientExtract> patientExtract
    {
        PatientExtract = patientExtract;
    }
}

public class SavePatientCommandHandler : IRequestHandler<SavePatientCommand, Result>
{
    private readonly IPatientExtractRepository _patientExtractRepository;

    public SavePatientCommandHandler(IPatientExtractRepository patientExtractRepository)
    {
        _patientExtractRepository = patientExtractRepository;
    }

    public async Task<Result> Handle(SavePatientCommand request, CancellationToken cancellationToken)
    {
        await _patientExtractRepository.MergeAsync(request.PatientExtract);


        return Result.Success();
    }
}
