using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Model.Stage;
using DwapiCentral.Mnch.Domain.Repository;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Application.Commands;

public class MergePatientMnchExtractCommand : IRequest<Result>
{
    public IEnumerable<PatientMnchExtract> MnchPatients { get; set; }

    public MergePatientMnchExtractCommand(IEnumerable<PatientMnchExtract> mnchPatients)
    {
        MnchPatients = mnchPatients;
    }
}

public class MergePatientMnchExtractCommandHandler : IRequestHandler<MergePatientMnchExtractCommand, Result>
{
    private readonly IStagePatientMnchRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergePatientMnchExtractCommandHandler(IStagePatientMnchRepository htsClientRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = htsClientRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergePatientMnchExtractCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.MnchPatients.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StagePatientMnchExtract>>(request.MnchPatients);


        if (extracts.Any())
        {
            StandardizeClass<StagePatientMnchExtract> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}

