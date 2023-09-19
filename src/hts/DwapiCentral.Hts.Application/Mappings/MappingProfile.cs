using AutoMapper;
using DwapiCentral.Hts.Domain.Model;
using DwapiCentral.Hts.Domain.Model.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //central => stage
            CreateMap<HtsClient, StageHtsClient>();
            CreateMap<HtsClientTest, StageHtsClientTest>();
            CreateMap<HtsClientLinkage, StageHtsClientLinkage>();
            CreateMap<HtsTestKit, StageHtsTestKit>();
            CreateMap<HtsClientTracing, StageHtsClientTracing>();
            CreateMap<HtsPartnerTracing, StageHtsPartnerTracing>();
            CreateMap<HtsPartnerNotificationServices, StageHtsPartnerNotificationServices>();
            CreateMap<HtsEligibilityScreening, StageHtsEligibilityScreening>();



            //stage => centralDb
            CreateMap<StageHtsClient, HtsClient>();
            CreateMap<StageHtsClientTest,HtsClientTest>();
            CreateMap<StageHtsClientLinkage,HtsClientLinkage>();
            CreateMap<StageHtsTestKit, HtsTestKit>();
            CreateMap<StageHtsClientTracing, HtsClientTracing>();
            CreateMap<StageHtsPartnerTracing, HtsPartnerTracing>();
            CreateMap<StageHtsPartnerNotificationServices, HtsPartnerNotificationServices>();
            CreateMap<StageHtsEligibilityScreening, HtsEligibilityScreening>();

        }
    }
}
