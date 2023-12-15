using CSharpFunctionalExtensions;
using DwapiCentral.Prep.Application.DTOs;
using DwapiCentral.Prep.Application.Events;
using DwapiCentral.Prep.Domain.Events;
using DwapiCentral.Prep.Domain.Exceptions;
using DwapiCentral.Prep.Domain.Repository;
using DwapiCentral.Shared.Domain.Enums;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Application.Commands;

public class ProcessHandshakeCommand : IRequest<Result>
{
    public Guid Session { get; set; }

    public ProcessHandshakeCommand(Guid session)
    {

        Session = session;
    }
}

public class ProcessHandshakeCommandHandler : IRequestHandler<ProcessHandshakeCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IManifestRepository _manifestRepository;



    public ProcessHandshakeCommandHandler(IMediator mediator, IManifestRepository manifestRepository)
    {
        _mediator = mediator;
        _manifestRepository = manifestRepository;
        
    }


    public async Task<Result> Handle(ProcessHandshakeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // check if manifest exists             
            var manifest = await _manifestRepository.GetById(request.Session);
            if (null == manifest)
                throw new ManifestNotFoundException(request.Session);

            // update handshake 

            manifest.SetHandshake();

            await _manifestRepository.Update(manifest);

            // Publish event...
            var notification = new HandshakeReceivedEvent
            {
                ManifestId = manifest.Id,
                Docket = manifest.Docket,
                SiteCode = manifest.SiteCode,
                Status = manifest.Status,
                Name = "HandShake",
                Date = DateTime.Now
            };
            await _mediator.Publish(notification);


            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e, "Update manifest error");
            return Result.Failure(e.Message);
        }
    }
}
