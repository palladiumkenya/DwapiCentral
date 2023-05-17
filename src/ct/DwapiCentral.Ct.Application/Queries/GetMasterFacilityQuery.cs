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
    private readonly IMediator _mediator;
    private readonly IMasterFacilityRepository _repository;

    public GetMasterFacilityQueryHandler(IMediator mediator, IMasterFacilityRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<Result<MasterFacility>> Handle(GetMasterFacilityQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var fac = await _repository.GetByCode(request.Code);
            
            if (null == fac)
                throw new SiteNotFoundException(request.Code);
            
            return Result.Success(fac);
        }
        catch (Exception e)
        {
            Log.Error(e, "error reading");
            return Result.Failure<MasterFacility>(e.Message);
        }

    }
}