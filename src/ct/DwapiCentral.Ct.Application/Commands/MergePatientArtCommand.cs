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

public class MergePatientArtCommand : IRequest<Result>
{
    public IEnumerable<PatientArtExtract> PatientArtExtracts { get; set; }

    public MergePatientArtCommand(IEnumerable<PatientArtExtract> patientArtExtracts)
    {
        PatientArtExtracts = patientArtExtracts;
    }
}

public class MergePatientArtCommandHandler : IRequestHandler<MergePatientArtCommand, Result>
{

    private readonly IPatientArtExtractRepositorycs _patientArtExtractRepositorycs;

    public MergePatientArtCommandHandler(IPatientArtExtractRepositorycs patientArtExtractRepositorycs)
    {
        _patientArtExtractRepositorycs= patientArtExtractRepositorycs;
    }

    public async Task<Result> Handle(MergePatientArtCommand request, CancellationToken cancellationToken)
    {
        await _patientArtExtractRepositorycs.MergPatientArt(request.PatientArtExtracts);
        return Result.Success();
    }
}
