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
    public PatientVisitExtract PatientVisits;

    public MergePatientVisitCommand(PatientVisitExtract patientVisits)
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
        var patientVisit = await _patientVisitRepository
            .GetByPatientDetails(request.PatientVisits.PatientPk, request.PatientVisits.SiteCode,request.PatientVisits.VisitId, request.PatientVisits.VisitDate);

        if (patientVisit != null)
        {
            //Update
            patientVisit.VisitDate = DateTime.Now;
            patientVisit.Weight= request.PatientVisits.Weight;
            patientVisit.Height= request.PatientVisits.Height;

            await _patientVisitRepository.MergeAsync((IEnumerable<PatientVisitExtract>)patientVisit);
        }
        else
        {
            //Add a new Visit
            var newpatientVisit = new PatientVisitExtract
            {
                Id = Guid.NewGuid(),
                Weight= request.PatientVisits.Weight,
                Height= request.PatientVisits.Height,
                PatientPk= request.PatientVisits.PatientPk,
                SiteCode= request.PatientVisits.SiteCode,
                VisitDate= request.PatientVisits.VisitDate
                

            };

            await _patientVisitRepository.MergeAsync((IEnumerable<PatientVisitExtract>)newpatientVisit);

            
        }
        return Result.Success();

    }
}
