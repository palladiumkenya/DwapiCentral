using CSharpFunctionalExtensions;
using DwapiCentral.Mnch.Application.DTOs;
using DwapiCentral.Mnch.Application.Events;
using DwapiCentral.Mnch.Domain.Events;
using DwapiCentral.Mnch.Domain.Exceptions;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Repository;
using DwapiCentral.Shared.Domain.Enums;
using log4net;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Application.Commands;

public class SaveManifestCommand : IRequest<Result>
{
    public Manifest Manifest { get; set; }

    public SaveManifestCommand(Manifest manifest)
    {
        Manifest = manifest;
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
            var facility = await _facilityRepository.GetByCode(request.Manifest.SiteCode);
            if (null == facility)
                throw new SiteNotEnrolledException(request.Manifest.SiteCode);


                var communityManifests = request.Manifest.EmrSetup == EmrSetup.Community;

                var otherManifests = request.Manifest.EmrSetup != EmrSetup.Community;

                try
                {
                    if (otherManifests != null)
                        _manifestRepository.ClearFacility(request.Manifest.SiteCode);
                }
                catch (Exception e)
                {
                    Log.Error("Clear MANIFEST ERROR ", e);
                }
                try
                {
                    if (communityManifests != null)
                        _manifestRepository.ClearFacility(request.Manifest.SiteCode, "IRDO");
                }
                catch (Exception e)
                {
                    Log.Error("Clear COMMUNITY MANIFEST ERROR ", e);
                }
                    request.Manifest.Recieved = request.Manifest.Cargoes
                    .Where(cargo => cargo.Type == 0)
                    .SelectMany(cargo => cargo.Items.Split(','))
                    .Count();

                    try
                    {
                        // Notify Spot
                        var metricDtos = MetricDto.Generate(request.Manifest);
                        if (metricDtos.Any())
                        {
                            var metrics = new MnchMetricsEvent
                            {
                                MnchMetricExtracts = metricDtos,

                            };
                            await _mediator.Publish(metrics, cancellationToken);
                        }

                        await _manifestRepository.Save(request.Manifest);

                        // notify spot => manifest
                        var notification = new ManifestReceivedEvent
                        {
                            ManifestId = request.Manifest.Id,
                            SiteCode = request.Manifest.SiteCode,
                            Docket = request.Manifest.Docket,
                            Status = request.Manifest.Status,
                            EmrSetup = request.Manifest.EmrSetup,
                            EmrVersion = request.Manifest.EmrVersion,
                            DwapiVersion = request.Manifest.DwapiVersion,                            
                            Metrics = request.Manifest.Cargoes
                        };
                        await _mediator.Publish(notification, cancellationToken);

                    }
                    catch (Exception e)
                    {
                        Log.Error(e.Message);
                    }
            return Result.Success();

        }
        catch(Exception e)
        {
            Log.Error(e, "save manifest error");
            return Result.Failure(e.Message);
        }

    }
}
