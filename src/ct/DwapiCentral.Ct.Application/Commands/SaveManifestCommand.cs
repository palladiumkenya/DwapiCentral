using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Exceptions;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Domain.Repository.Stage;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.Commands;

public class SaveManifestCommand : IRequest<Result>
{
   public Manifest manifest { get; set; }

   public SaveManifestCommand(Manifest manifest)
   {
       this.manifest = manifest;
   }
}

public class SaveManifestCommandHandler : IRequestHandler<SaveManifestCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IManifestRepository _manifestRepository;
    private readonly IFacilityRepository _facilityRepository;
    private readonly IStagePatientExtractRepository _stagePatientExtractRepository;

    public SaveManifestCommandHandler(IMediator mediator, IManifestRepository manifestRepository, IFacilityRepository facilityRepository, IStagePatientExtractRepository stagePatientExtractRepository)
    {
        _mediator = mediator;
        _manifestRepository = manifestRepository;
        _facilityRepository = facilityRepository;
        _stagePatientExtractRepository = stagePatientExtractRepository;
    }

    public async Task<Result> Handle(SaveManifestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var facility = await _facilityRepository.GetByCode(request.manifest.SiteCode);
            if (null == facility)
                throw new SiteNotEnrolledException(request.manifest.SiteCode);

            var manifest = await _manifestRepository.GetById(request.manifest.Id);
            if (null != manifest)
                throw new ManifestAlreadyExistsException(request.manifest.Id);

            await _manifestRepository.Save(request.manifest);

            await _stagePatientExtractRepository.ClearSite(request.manifest.SiteCode);

            await _mediator.Publish(new ManifestReceivedEvent(request.manifest.Id, request.manifest.SiteCode), cancellationToken);

            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e,"save manifest error");
            return Result.Failure(e.Message);
        }

    }
}


