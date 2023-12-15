using AutoMapper;
using DwapiCentral.Mnch.Domain.Model;
using DwapiCentral.Mnch.Domain.Model.Stage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //central => stage
            CreateMap<PatientMnchExtract, StagePatientMnchExtract>();
            CreateMap<AncVisit, StageAncVisit>();
            CreateMap<CwcEnrolment, StageCwcEnrolment>();
            CreateMap<CwcVisit, StageCwcVisit>();
            CreateMap<HeiExtract, StageHeiExtract>();
            CreateMap<MatVisit, StageMatVisit>();
            CreateMap<MnchArt, StageMnchArt>();
            CreateMap<MnchEnrolment, StageMnchEnrolment>();
            CreateMap<MnchImmunization, StageMnchImmunization>();
            CreateMap<MnchLab, StageMnchLab>();
            CreateMap<MotherBabyPair, StageMotherBabyPair>();
            CreateMap<PncVisit, StagePncVisit>();



            //stage => centralDb
            CreateMap<StagePatientMnchExtract, PatientMnchExtract>();
            CreateMap<StageAncVisit, AncVisit>();
            CreateMap<StageCwcEnrolment, CwcEnrolment>();
            CreateMap<StageCwcVisit, CwcVisit>();
            CreateMap<StageHeiExtract, HeiExtract>();
            CreateMap<StageMatVisit, MatVisit>();
            CreateMap<StageMnchArt, MnchArt>();
            CreateMap<StageMnchEnrolment, MnchEnrolment>();
            CreateMap<StageMnchImmunization, MnchImmunization>();
            CreateMap<StageMnchLab, MnchLab>();
            CreateMap<StageMotherBabyPair, MotherBabyPair>();
            CreateMap<StagePncVisit, PncVisit>();
        }
    }
}
