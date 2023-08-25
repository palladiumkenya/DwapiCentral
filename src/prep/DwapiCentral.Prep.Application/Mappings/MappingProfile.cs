using AutoMapper;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Models.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //central => stage
            CreateMap<PatientPrep, StagePatientPrep>();
            CreateMap<PrepAdverseEvent, StagePrepAdverseEvent>();
            CreateMap<PrepBehaviourRisk, StagePrepBehaviourRisk>();
            CreateMap<PrepCareTermination, StagePrepCareTermination>();
            CreateMap<PrepLab, StagePrepLab>();
            CreateMap<PrepPharmacy, StagePrepPharmacy>();
            CreateMap<PrepVisit, StagePrepVisit>();


            //stage => centralDb
            CreateMap<StagePatientPrep, PatientPrep>();
            CreateMap<StagePrepAdverseEvent, PrepAdverseEvent>();
            CreateMap<StagePrepBehaviourRisk, PrepBehaviourRisk>();
            CreateMap<StagePrepCareTermination, PrepCareTermination>();
            CreateMap<StagePrepLab, PrepLab>();
            CreateMap<StagePrepPharmacy, PrepPharmacy>();
            CreateMap<StagePrepVisit, PrepVisit>();
        }
    }
}
