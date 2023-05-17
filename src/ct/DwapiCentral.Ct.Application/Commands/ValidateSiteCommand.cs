using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Exceptions;
using DwapiCentral.Ct.Domain.Models;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.Commands;

public class ValidateSiteCommand : IRequest<Result>
{
    public int SiteCode { get; set; }
    public string SiteName { get; set; }

    public ValidateSiteCommand(int siteCode, string siteName)
    {
        SiteCode = siteCode;
        SiteName = siteName;
    }
}

public class ValidateSiteCommandHandler : IRequestHandler<ValidateSiteCommand, Result>
{
    private readonly IMediator _mediator;
    private readonly IMasterFacilityRepository _masterFacilityRepository;
    private readonly IFacilityRepository _facilityRepository;

    public ValidateSiteCommandHandler(IMediator mediator, IMasterFacilityRepository masterFacilityRepository, IFacilityRepository facilityRepository)
    {
        _mediator = mediator;
        _masterFacilityRepository = masterFacilityRepository;
        _facilityRepository = facilityRepository;
    }

    public async Task<Result> Handle(ValidateSiteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var masterSite = await _masterFacilityRepository.GetByCode(request.SiteCode);
            if (null == masterSite)
                throw new SiteNotFoundException(request.SiteCode);
            
            var site= await _facilityRepository.GetByCode(request.SiteCode);
            if (null == site)
            {
                var facility = new Facility(request.SiteCode, request.SiteName);
                await _facilityRepository.Save(facility);
                await _mediator.Publish(new SiteEnrolledEvent(request.SiteCode, request.SiteName), cancellationToken);
            }

            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e,"validate site error");
            return Result.Failure(e.Message);
        }

    }
}


