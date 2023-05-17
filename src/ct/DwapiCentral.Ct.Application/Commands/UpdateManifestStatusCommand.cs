using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Exceptions;
using DwapiCentral.Ct.Domain.Models;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.Commands;

public class UpdateManifestStatusCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Status { get; set; }

    public UpdateManifestStatusCommand(Guid id, string status)
    {
        Id = id;
        Status = status;
    }
}

public class UpdateManifestStatusCommandHandler : IRequestHandler<UpdateManifestStatusCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IManifestRepository _manifestRepository;

    public UpdateManifestStatusCommandHandler(IMediator mediator, IManifestRepository manifestRepository)
    {
        _mediator = mediator;
        _manifestRepository = manifestRepository;
    }

    public async Task<Result> Handle(UpdateManifestStatusCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var manifest = await _manifestRepository.GetById(request.Id);
            if (null == manifest)
                throw new ManifestNotFoundException(request.Id);

            manifest.UpdateStatus(request.Status);
            
            await _manifestRepository.Update(manifest);

            await _mediator.Publish(new ManifestStatusUpdatedEvent(request.Id, request.Status), cancellationToken);
            
            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e,"Update manifest error");
            return Result.Failure(e.Message);
        }

    }
}


