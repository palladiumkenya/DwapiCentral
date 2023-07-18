using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Exceptions;
using DwapiCentral.Ct.Domain.Models;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.Commands;

public class UpdateHandshakeCommand : IRequest<Result>
{
    
    public Guid Session { get; set; }

    public UpdateHandshakeCommand(Guid session)
    {
       
        Session = session;
    }
}

public class UpdateHandshakeCommandHandler : IRequestHandler<UpdateHandshakeCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IManifestRepository _manifestRepository;

    public UpdateHandshakeCommandHandler(IMediator mediator, IManifestRepository manifestRepository)
    {
        _mediator = mediator;
        _manifestRepository = manifestRepository;
    }

    public async Task<Result> Handle(UpdateHandshakeCommand request, CancellationToken cancellationToken)
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
            
            await _mediator.Publish(new HandshakeReceivedEvent(manifest.Id, manifest.SiteCode), cancellationToken);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e,"Update manifest error");
            return Result.Failure(e.Message);
        }

    }
}


