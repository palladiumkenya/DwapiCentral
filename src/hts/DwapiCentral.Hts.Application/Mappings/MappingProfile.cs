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



            //stage => centralDb
            CreateMap<StageHtsClient, HtsClient>();
        }
    }
}
