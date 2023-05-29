using AutoMapper;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Mappings
{
    public class MappingProfile :  Profile
    {
        
        public MappingProfile()
        {
            CreateMap<PatientSourceDto, PatientExtract>();
            
        }
    }
}
