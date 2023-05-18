using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Domain.Exceptions;
using DwapiCentral.Ct.Domain.Models;
using MediatR;
using Serilog;

namespace DwapiCentral.Ct.Application.Queries;

public class GetMasterFacilityQuery:IRequest<Result<MasterFacility>>
{
    public int Code { get; set; }

    public GetMasterFacilityQuery(int code)
    {
        Code = code;
    }
}

public class GetMasterFacilityQueryHandler:IRequestHandler<GetMasterFacilityQuery,Result<MasterFacility>>
{
    private readonly IMasterFacilityRepository _masterFacilityRepository;

    public GetMasterFacilityQueryHandler(IMasterFacilityRepository masterFacilityRepository)
    {
        _masterFacilityRepository = masterFacilityRepository;
    }

    public async Task<Result<MasterFacility>> Handle(GetMasterFacilityQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var masterFacility = await _masterFacilityRepository.GetByCode(request.Code);
            
            if (null == masterFacility)
                throw new SiteNotFoundInMflException(request.Code);
            
            return Result.Success(masterFacility);
        }
        catch (Exception e)
        {
            Log.Error(e, "error reading");
            return Result.Failure<MasterFacility>(e.Message);
        }

    }
}