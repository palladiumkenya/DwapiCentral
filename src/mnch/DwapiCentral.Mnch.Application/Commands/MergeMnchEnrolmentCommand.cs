using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Mnch.Domain.Model.Stage;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DwapiCentral.Mnch.Domain.Repository;

namespace DwapiCentral.Mnch.Application.Commands;

public class MergeMnchEnrolmentCommand : IRequest<Result>
{
    public IEnumerable<MnchEnrolment> MnchEnrolments { get; set; }

    public MergeMnchEnrolmentCommand(IEnumerable<MnchEnrolment> mnchEnrolments)
    {
        MnchEnrolments = mnchEnrolments;
    }
}
public class MergeMnchEnrolmentCommandHandler : IRequestHandler<MergeMnchEnrolmentCommand, Result>
{
    private readonly IStageMnchEnrolmentRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeMnchEnrolmentCommandHandler(IStageMnchEnrolmentRepository mnchEnrolmentRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = mnchEnrolmentRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeMnchEnrolmentCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.MnchEnrolments.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageMnchEnrolment>>(request.MnchEnrolments);


        if (extracts.Any())
        {
            StandardizeClass<StageMnchEnrolment> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}
