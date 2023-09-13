using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Models.Stage;
using DwapiCentral.Prep.Domain.Repository;
using DwapiCentral.Prep.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Application.Commands;

public class MergePatientPrepCommand : IRequest<Result>
{
    public IEnumerable<PatientPrepExtract> PatientPreps { get; set; }

    public MergePatientPrepCommand(IEnumerable<PatientPrepExtract> patientPreps)
    {
        PatientPreps = patientPreps;
    }
}

public class MergePatientPrepCommandHandler : IRequestHandler<MergePatientPrepCommand, Result>
{
    private readonly IStagePatientPrepRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergePatientPrepCommandHandler(IStagePatientPrepRepository hpatientPrepRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = hpatientPrepRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePatientPrepCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.PatientPreps.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StagePatientPrep>>(request.PatientPreps);


        if (extracts.Any())
        {
            StandardizeClass<StagePatientPrep> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}

