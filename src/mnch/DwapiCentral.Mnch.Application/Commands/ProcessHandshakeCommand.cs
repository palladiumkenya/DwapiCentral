using CSharpFunctionalExtensions;
using DwapiCentral.Mnch.Application.DTOs;
using DwapiCentral.Mnch.Application.Events;
using DwapiCentral.Mnch.Domain.Events;
using DwapiCentral.Mnch.Domain.Exceptions;
using DwapiCentral.Mnch.Domain.Repository;
using DwapiCentral.Shared.Domain.Enums;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DwapiCentral.Mnch.Application.Commands;

public class ProcessHandshakeCommand : IRequest<Result>
{
    public Guid Session { get; set; }

    public ProcessHandshakeCommand(Guid session)
    {

        Session = session;
    }
}

public class ProcessManifestCommandHandler : IRequestHandler<ProcessHandshakeCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IManifestRepository _manifestRepository;
    


    public ProcessManifestCommandHandler(IMediator mediator, IManifestRepository manifestRepository, IFacilityRepository facilityRepository)
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
