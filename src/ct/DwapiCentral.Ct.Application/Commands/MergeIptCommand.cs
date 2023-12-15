using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs.Source;
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

public class MergeIptCommand : IRequest<Result>
{

    public PatientIptSourceBag IptExtracts { get; set; }

    public MergeIptCommand(PatientIptSourceBag iptExtracts)
    {
        IptExtracts = iptExtracts;
    }

}

public class MergeIptCommandHandler : IRequestHandler<MergeIptCommand, Result>
{
    private readonly IStageIptExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeIptCommandHandler(IStageIptExtractRepository iptRepository, IMapper mapper)
    {
        _stageRepository = iptRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeIptCommand request, CancellationToken cancellationToken)
    {
        //await _iptRepository.MergeAsync(request.IptExtracts);
        var extracts = _mapper.Map<List<StageIptExtract>>(request.IptExtracts.Extracts);
        if (extracts.Any())
        {
            StandardizeClass<StageIptExtract, PatientIptSourceBag> standardizer = new(extracts, request.IptExtracts);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _stageRepository.SyncStage(extracts, request.IptExtracts.ManifestId.Value);

        return Result.Success();

    }

}
