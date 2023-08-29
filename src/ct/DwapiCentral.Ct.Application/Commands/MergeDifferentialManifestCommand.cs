using CSharpFunctionalExtensions;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands;

public class MergeDifferentialManifestCommand : IRequest<Result>
{

    public FacilityManifest manifest { get; set; }

    public MergeDifferentialManifestCommand(FacilityManifest manifest)
    {
        this.manifest = manifest;
    }
}

public class MergeDifferentialManifestCommandHandler : IRequestHandler<MergeDifferentialManifestCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IManifestRepository _manifestRepository;
    private readonly IFacilityRepository _facilityRepository;
    private readonly IPatientExtractRepository _patientExtractRepository;
    private readonly JsonSerializerSettings _serializerSettings;

    public MergeDifferentialManifestCommandHandler(IMediator mediator, IManifestRepository manifestRepository, IFacilityRepository facilityRepository, IPatientExtractRepository patientExtractRepository)
    {
        _mediator = mediator;
        _manifestRepository = manifestRepository;
        _facilityRepository = facilityRepository;
        _patientExtractRepository = patientExtractRepository;
        _serializerSettings = new JsonSerializerSettings()
        { ContractResolver = new CamelCasePropertyNamesContractResolver() };
    }

    public async Task<Result> Handle(MergeDifferentialManifestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var facility = await _facilityRepository.GetByCode(request.manifest.SiteCode);
            if (null == facility)
                throw new SiteNotEnrolledException(request.manifest.SiteCode);

            var manifest = await _manifestRepository.GetById(request.manifest.Id);
            if (null != manifest)
                throw new ManifestAlreadyExistsException(request.manifest.Id);

            await _patientExtractRepository.processDifferentialPatients(request.manifest);

            //notify spot
            Log.Debug("posting to SPOT...");          
            var facManifest = Manifest.Create(request.manifest);
            var manifestDto = new ManifestDto(facManifest, request.manifest);
            var metrics = MetricDto.Generate(facManifest);
            var metricDtos = metrics.Where(x => x.CargoType != CargoType.Indicator).ToList();
            var indicatorDtos = metrics.Where(x => x.CargoType == CargoType.Indicator).ToList();
            manifestDto.Cargo =
                JsonConvert.SerializeObject(ExtractDto.GenerateCargo(metricDtos), _serializerSettings);
           
            var notification = new ManifestDtoEvent
            {
               manifestDtoEvent = manifestDto
            };
            await _mediator.Publish(notification, cancellationToken);
           

            if (metricDtos.Any())
            {
                var metricEvent = new MetricsExtractedEvent { metricDtos = metricDtos };
                await _mediator.Publish(metricEvent, cancellationToken);
            }
            
            if (indicatorDtos.Any())
            {
                var indstats = IndicatorDto.Generate(indicatorDtos);
                var indicators = new IndicatorsExtractedEvent
                {
                    IndicatorsExtracts = indstats,

                };
                await _mediator.Publish(indicators, cancellationToken);
            }

            await _manifestRepository.Save(facManifest);

            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e, "save manifest error");
            return Result.Failure(e.Message);
        }

    }
}

