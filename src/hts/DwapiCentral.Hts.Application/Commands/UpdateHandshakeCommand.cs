using CSharpFunctionalExtensions;
using DwapiCentral.Contracts.Common;
using DwapiCentral.Hts.Domain.Events;
using DwapiCentral.Hts.Domain.Exceptions;
using DwapiCentral.Hts.Domain.Repository;
using MediatR;
using Serilog;

namespace DwapiCentral.Hts.Application.Commands;

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


