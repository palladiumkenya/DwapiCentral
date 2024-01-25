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
            CreateMap<StageHtsClient, HtsClient>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); ;
            CreateMap<StageHtsClientTest,HtsClientTest>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); ;
            CreateMap<StageHtsClientLinkage,HtsClientLinkage>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); ;
            CreateMap<StageHtsTestKit, HtsTestKit>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); ;
            CreateMap<StageHtsClientTracing, HtsClientTracing>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); ;
            CreateMap<StageHtsPartnerTracing, HtsPartnerTracing>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); ;
            CreateMap<StageHtsPartnerNotificationServices, HtsPartnerNotificationServices>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); ;
            CreateMap<StageHtsEligibilityScreening, HtsEligibilityScreening>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); ;

        }
    }
}
