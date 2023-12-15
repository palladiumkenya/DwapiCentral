using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Model.Stage;
using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Hts.Domain.Repository.Stage;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Application.Commands;

public class MergeHtsPNSCommand : IRequest<Result>
{
    public IEnumerable<HtsPartnerNotificationServices> PartnerNotificationServices { get; set; }

    public MergeHtsPNSCommand(IEnumerable<HtsPartnerNotificationServices> partnernotificationservices)
    {

        PartnerNotificationServices = partnernotificationservices;
    }
}


public class MergeHtsPNSCommandHandler : IRequestHandler<MergeHtsPNSCommand, Result>
{
    private readonly IStageHtsPartnerNotificationServicesRepository _Repository;
    private readonly IManifestRepository _manifestRepository;
    private readonly IMapper _mapper;


    public MergeHtsPNSCommandHandler(IStageHtsPartnerNotificationServicesRepository htsClientRepository, IManifestRepository manifestRepository, IMapper mapper)
    {
        _Repository = htsClientRepository;
        _manifestRepository = manifestRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeHtsPNSCommand request, CancellationToken cancellationToken)
    {
        var manifestId = await _manifestRepository.GetManifestId(request.PartnerNotificationServices.FirstOrDefault().SiteCode);

        var extracts = _mapper.Map<List<StageHtsPartnerNotificationServices>>(request.PartnerNotificationServices);


        if (extracts.Any())
        {
            StandardizeClass<StageHtsPartnerNotificationServices> standardizer = new(extracts, manifestId);
            standardizer.StandardizeExtracts();

        }
        //stage
        await _Repository.SyncStage(extracts, manifestId);


        return Result.Success();
    }

}
