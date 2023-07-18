using CSharpFunctionalExtensions;
using DwapiCentral.Contracts.Common;
using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Events;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Exceptions;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using DwapiCentral.Ct.Domain.Repository.Stage;
using DwapiCentral.Shared.Domain.Enums;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.Commands;

public class SaveManifestCommand : IRequest<Result>
{
   public FacilityManifest manifest { get; set; }

   public SaveManifestCommand(FacilityManifest manifest)
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

            var facManifest = Manifest.Create(request.manifest);
            await _manifestRepository.Save(facManifest);
            //notify spot=> Indicators            
            var indicatorDtos = facManifest.Metrics.Where(x => x.Type == CargoType.Indicator).ToList();
            if (indicatorDtos.Any())
            {
                var indicatorstats = IndicatorDto.Generate(indicatorDtos);
                var indicators = new IndicatorsExtractedEvent
                {
                    IndicatorsExtracts = indicatorstats,
                    
                };
                await _mediator.Publish(indicators,  cancellationToken);
            }   

            // notify spot => metrics
            var notification = new ManifestReceivedEvent
            {
                ManifestId = facManifest.Id,
                SiteCode = facManifest.SiteCode,
                Docket = facManifest.Docket,
                UploadMode = facManifest.UploadMode,
                Status = facManifest.Status,
                EmrSetup = facManifest.EmrSetup,
                EmrVersion = facManifest.EmrVersion,
                DwapiVersion = facManifest.DwapiVersion,
                Metrics = facManifest.Metrics
            };
            await _mediator.Publish(notification, cancellationToken);

            await _stagePatientExtractRepository.ClearSite(request.manifest.SiteCode);


            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e,"save manifest error");
            return Result.Failure(e.Message);
        }

    }
}


