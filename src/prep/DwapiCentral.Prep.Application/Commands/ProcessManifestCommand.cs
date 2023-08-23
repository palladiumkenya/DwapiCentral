using CSharpFunctionalExtensions;
using DwapiCentral.Prep.Application.DTOs;
using DwapiCentral.Prep.Application.Events;
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

public class ProcessManifestCommand : IRequest<Result>
{
    public int SiteCode { get; set; }

    public ProcessManifestCommand(int siteCode)
    {
        SiteCode = siteCode;
    }
}

public class ProcessManifestCommandHandler : IRequestHandler<ProcessManifestCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IManifestRepository _manifestRepository;
    private readonly IFacilityRepository _facilityRepository;


    public ProcessManifestCommandHandler(IMediator mediator, IManifestRepository manifestRepository, IFacilityRepository facilityRepository)
    {
        _mediator = mediator;
        _manifestRepository = manifestRepository;
        _facilityRepository = facilityRepository;
    }


    public async Task<Result> Handle(ProcessManifestCommand request, CancellationToken cancellationToken)
    {
        var manifests = _manifestRepository.GetStaged(request.SiteCode).ToList();
        if (manifests.Any())
        {
            var communityManifests = manifests.Where(x => x.EmrSetup == EmrSetup.Community).ToList();

            var otherManifests = manifests.Where(x => x.EmrSetup != EmrSetup.Community).ToList();

            try
            {
                if (otherManifests.Any())
                    _manifestRepository.ClearFacility(request.SiteCode);
            }
            catch (Exception e)
            {
                Log.Error("Clear MANIFEST ERROR ", e);
            }
            try
            {
                if (communityManifests.Any())
                    _manifestRepository.ClearFacility(request.SiteCode, "IRDO");
            }
            catch (Exception e)
            {
                Log.Error("Clear COMMUNITY MANIFEST ERROR ", e);
            }

            foreach (var manifest in manifests)
            {
                var clientCount = _manifestRepository.GetPatientCount(manifest.Id);
                _manifestRepository.updateCount(manifest.Id, clientCount);
               
                try
                {
                   // Notify Spot
                    var metricDtos = MetricDto.Generate(manifest);
                    if (metricDtos.Any())
                    {
                        var metrics = new PrepMetricsEvent
                        {
                            PrepMetricExtracts = metricDtos,

                        };
                        await _mediator.Publish(metrics, cancellationToken);
                    }

                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                }

            }
            return Result.Success();
        }

        else
        {
            return Result.Failure($"process manifest of sitecode {request.SiteCode} failed");
        }
    }
}
