﻿using AutoMapper;
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

public class MergeCwcEnrolmentCommand : IRequest<Result>
{
    public IEnumerable<CwcEnrolment> CwcEnrolments { get; set; }

    public MergeCwcEnrolmentCommand(IEnumerable<CwcEnrolment> cwcEnrolments)
    {
        CwcEnrolments = cwcEnrolments;
    }
}
public class MergeCwcEnrolmentCommandHandler : IRequestHandler<MergeCwcEnrolmentCommand, Result>
{
    private readonly IStageCwcEnrolmentRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeCwcEnrolmentCommandHandler(IStageCwcEnrolmentRepository cwcEnrolmentRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = cwcEnrolmentRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeCwcEnrolmentCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.CwcEnrolments.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageCwcEnrolment>>(request.CwcEnrolments);


        if (extracts.Any())
        {
            StandardizeClass<StageCwcEnrolment> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }
}


