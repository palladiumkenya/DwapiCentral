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
    public PatientPharmacyExtract PatientPharmacy { get; set; } 

    public AddOrUpdatePatientPharmacyCommand(PatientPharmacyExtract patientPharmacy)
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

       var PatientPharmacy = new PatientPharmacyExtract
       {
           PatientPk = request.PatientPharmacy.PatientPk,
           SiteCode = request.PatientPharmacy.SiteCode,
           VisitID = request.PatientPharmacy.VisitID,
           DispenseDate = request.PatientPharmacy.DispenseDate,
           Drug = request.PatientPharmacy.Drug,
           Duration = request.PatientPharmacy.Duration

       };

        await _patientPharmacyRepository.MergePharmacyExtractsAsync((IEnumerable<PatientPharmacyExtract>)PatientPharmacy);

        return Result.Success();
    }
}
