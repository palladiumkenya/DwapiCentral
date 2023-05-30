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

public class AddOrUpdatePatientPharmacyCommand : IRequest<Result>
{
    public IEnumerable<PatientPharmacyExtract> PatientPharmacy { get; set; } 

    public AddOrUpdatePatientPharmacyCommand(IEnumerable<PatientPharmacyExtract> patientPharmacy)
    {
        PatientPharmacy= patientPharmacy;
    }
}

public class AddOrUpdatePatientPharmacyCommandHandler : IRequestHandler<AddOrUpdatePatientPharmacyCommand, Result>
{
    private readonly IPatientPharmacyRepository _patientPharmacyRepository;

    public AddOrUpdatePatientPharmacyCommandHandler(IPatientPharmacyRepository patientPharmacyRepository)
    {
        _patientPharmacyRepository= patientPharmacyRepository;
    }

    public async Task<Result> Handle(AddOrUpdatePatientPharmacyCommand request, CancellationToken cancellationToken)
    {

    

        await _patientPharmacyRepository.MergePharmacyExtractsAsync(request.PatientPharmacy);

        return Result.Success();
    }
}
