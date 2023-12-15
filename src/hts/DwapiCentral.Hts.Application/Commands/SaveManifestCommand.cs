using CSharpFunctionalExtensions;
using DwapiCentral.Hts.Application.DTOs;
using DwapiCentral.Hts.Application.Events;
using DwapiCentral.Hts.Domain.Events;
using DwapiCentral.Hts.Domain.Exceptions;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Shared.Domain.Enums;
using log4net;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Application.Commands;

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
  

    public SaveManifestCommandHandler(IMediator mediator, IManifestRepository manifestRepository, IFacilityRepository facilityRepository)
    {
        _mediator = mediator;
        _manifestRepository = manifestRepository;
        _facilityRepository = facilityRepository;
    }

    public async Task<Result> Handle(SaveManifestCommand request, CancellationToken cancellationToken)
    {
        try
        {

           
            var facility = await _facilityRepository.GetByCode(request.manifest.SiteCode);
            if (null == facility)
                throw new SiteNotEnrolledException(request.manifest.SiteCode);

            try
            {
                if (request.manifest.EmrSetup != EmrSetup.Community)
                    await _manifestRepository.ClearFacility(request.manifest.SiteCode);
            }
            catch (Exception e)
            {
                Log.Error("Clear MANIFEST ERROR ", e);
            }

            try
            {               
                if (request.manifest.EmrSetup== EmrSetup.Community)
                    await _manifestRepository.ClearFacility(request.manifest.SiteCode, "IRDO");
            }
            catch (Exception e)
            {
                Log.Error("Clear COMMUNITY MANIFEST ERROR ", e);
            }


            await _manifestRepository.Save(request.manifest);
           
            // notify spot => manifest
            var notification = new ManifestReceivedEvent
            {
                ManifestId = request.manifest.Id,
                SiteCode = request.manifest.SiteCode,
                Docket = request.manifest.Docket,               
                Status = request.manifest.Status,
                EmrSetup = request.manifest.EmrSetup,
                EmrVersion = request.manifest.EmrVersion,
                DwapiVersion = request.manifest.DwapiVersion,
                Metrics = request.manifest.Cargoes
            };
            await _mediator.Publish(notification, cancellationToken);

            //notify spot => Hts metrics          
            var metricDtos = MetricDto.Generate(request.manifest);

            if (metricDtos.Any())
            {               
                var metrics = new HtsMetricsEvent
                {
                    HtsMetricExtracts = metricDtos,

                };
                await _mediator.Publish(metrics, cancellationToken);
            }

            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e, "save manifest error");
            return Result.Failure(e.Message);
        }

    }
}
