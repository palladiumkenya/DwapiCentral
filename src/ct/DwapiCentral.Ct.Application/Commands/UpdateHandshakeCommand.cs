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
    public Guid Id { get; set; }
    public int SiteCode { get; set; }
    public Guid? Session { get; set; }

    public UpdateHandshakeCommand(Guid id, int siteCode, Guid? session)
    {
        Id = id;
        SiteCode = siteCode;
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
            var manifest = await _manifestRepository.GetById(request.Id);
            if (null == manifest)
                throw new ManifestNotFoundException(request.Id);

            manifest.SetHandshake();
            
            await _manifestRepository.Update(manifest);

            await _mediator.Publish(new HandshakeReceivedEvent(request.Id, request.SiteCode), cancellationToken);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e,"Update manifest error");
            return Result.Failure(e.Message);
        }

    }
}


