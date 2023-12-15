using CSharpFunctionalExtensions;
using DwapiCentral.Hts.Domain.Events;
using DwapiCentral.Hts.Domain.Exceptions;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Repository;
using MediatR;
using Serilog;

namespace DwapiCentral.Hts.Application.Commands;

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
            // check if in Master facility list

            var masterFacility = await _masterFacilityRepository.GetByCode(request.SiteCode);
            if (null == masterFacility)
                throw new SiteNotFoundInMflException(request.SiteCode);

            // check if Enrolled

            var facility = await _facilityRepository.GetByCode(request.SiteCode);
            if (null == facility)
            {
                var newFacility = new Facility(request.SiteCode, request.SiteName);
                await _facilityRepository.Save(newFacility);

                // publish Event...

                await _mediator.Publish(new SiteEnrolledEvent(request.SiteCode, request.SiteName), cancellationToken);
            }

            return Result.Success();
        }
        catch (Exception e)
        {
            Log.Error(e, "validate site error");
            return Result.Failure(e.Message);
        }
    }
}


