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

public class MergePatientLabsCommand : IRequest<Result>
{
    
     
    public IEnumerable<PatientLaboratoryExtract> PatientLabs { get; set; }

    public MergePatientLabsCommand(IEnumerable<PatientLaboratoryExtract> patientLaboratoryExtract)
    {
        PatientLabs= patientLaboratoryExtract;
    }

}

public class MergePatientLabsCommandHandler : IRequestHandler<MergePatientLabsCommand, Result>
{
    private readonly IPatientLaboratoryExtractRepository _patientLaboratoryRepository;

    public MergePatientLabsCommandHandler(IPatientLaboratoryExtractRepository patientLaboratoryExtractRepository)
    {
        _patientLaboratoryRepository= patientLaboratoryExtractRepository;
    }

    public async Task<Result> Handle(MergePatientLabsCommand request, CancellationToken cancellationToken)
    {

        await _patientLaboratoryRepository.MergeLaboratoryExtracts(request.PatientLabs);

        return Result.Success();
       
    }
}
