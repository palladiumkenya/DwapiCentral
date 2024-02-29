using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Application.Hashing;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands;

public class MergePatientLabsCommand : IRequest<Result>
{
    
     
    public LaboratorySourceBag PatientLabsSourceBag { get; set; }

    public MergePatientLabsCommand(LaboratorySourceBag patientLaboratoryExtractsourceBag)
    {
        PatientLabsSourceBag = patientLaboratoryExtractsourceBag;
    }

}

public class MergePatientLabsCommandHandler : IRequestHandler<MergePatientLabsCommand, Result>
{
    private readonly IStageLaboratoryExtractRepository _stageRepository;    
    private readonly IMapper _mapper;

    public MergePatientLabsCommandHandler(IStageLaboratoryExtractRepository stageLaboratoryExtractRepository,IMapper mapper)
    {
        _stageRepository = stageLaboratoryExtractRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePatientLabsCommand request, CancellationToken cancellationToken)
    {
        var extracts = _mapper.Map<List<StageLaboratoryExtract>>(request.PatientLabsSourceBag.Extracts);

        if (extracts.Any())
        {
            StandardizeClass<StageLaboratoryExtract, LaboratorySourceBag> standardizer = new(extracts, request.PatientLabsSourceBag);
            standardizer.StandardizeExtracts();

        }

        Parallel.ForEach(extracts, extract =>
        {
            var concatenatedData = $"{extract.VisitId}{extract.OrderedByDate}{extract.TestResult}{extract.TestName}";
            var checksumHash = VisitsHash.ComputeChecksumHash(concatenatedData);
            extract.Mhash = Math.Abs(checksumHash);
        });

        //stage
        await _stageRepository.SyncStage(extracts, request.PatientLabsSourceBag.ManifestId.Value);



        return Result.Success();
       
    }
}
