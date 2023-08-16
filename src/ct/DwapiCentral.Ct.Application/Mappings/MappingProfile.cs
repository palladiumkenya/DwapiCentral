using AutoMapper;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Models.Extracts;
using DwapiCentral.Ct.Domain.Models.Stage;
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
            //Dto >> Extract
            CreateMap<PatientSourceDto, PatientExtract>();
            CreateMap<PatientVisitSourceDto, PatientVisitExtract>();
            CreateMap<PatientIptSourceDto,IptExtract>();
            CreateMap<PatientArtSourceDto, PatientArtExtract>();
            CreateMap<PatientBaselineSourceDto,PatientBaselinesExtract>();
            CreateMap<ContactListingSourceDto, ContactListingExtract>();
            CreateMap<CovidSourceDto, CovidExtract>();
            CreateMap<DefaulterTracingSourceDto, DefaulterTracingExtract>();
            CreateMap<DepressionScreeningSourceDto, DepressionScreeningExtract>();
            CreateMap<DrugAlcoholScreeningSourceDto,DrugAlcoholScreeningExtract>();
            CreateMap<EnhancedAdherenceCounselingSourceDto,EnhancedAdherenceCounsellingExtract>();
            CreateMap<GbvScreeningSourceDto, GbvScreeningExtract>();            
            CreateMap<LaboratorySourceDto, PatientLaboratoryExtract>();
            CreateMap<OtzSourceDto,OtzExtract>();
            CreateMap<OvcSourceDto,OvcExtract>();
            CreateMap<PharmacySourceDto, PatientPharmacyExtract>();
            CreateMap<StatusSourceDto, PatientStatusExtract>();
            CreateMap<AllergiesChronicIllnessSourceDto,AllergiesChronicIllnessExtract>();
            CreateMap<AdverseEventSourceDto,PatientAdverseEventExtract>();
            CreateMap<CervicalCancerScreeningSourceDto, CervicalCancerScreeningExtract>();
            CreateMap<IITRiskScoreSourceDto, IITRiskScore>();

            //stage >> Extract
            CreateMap<StagePatientExtract, PatientExtract>();
            CreateMap<StageArtExtract, PatientArtExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageLaboratoryExtract, PatientLaboratoryExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageAllergiesChronicIllnessExtract, AllergiesChronicIllnessExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); 
            CreateMap<StageAdverseEventExtract, PatientAdverseEventExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageBaselineExtract, PatientBaselinesExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); 
            CreateMap<StageContactListingExtract, ContactListingExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); 
            CreateMap<StageCovidExtract, CovidExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); 
            CreateMap<StageDefaulterTracingExtract, DefaulterTracingExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageDepressionScreeningExtract, DepressionScreeningExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageDrugAlcoholScreeningExtract, DrugAlcoholScreeningExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageEnhancedAdherenceCounsellingExtract, EnhancedAdherenceCounsellingExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); 
            CreateMap<StageGbvScreeningExtract, GbvScreeningExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid())); 
            CreateMap<StageIptExtract, IptExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageOtzExtract, OtzExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageOvcExtract, OvcExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StagePharmacyExtract, PatientPharmacyExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageStatusExtract, PatientStatusExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageVisitExtract, PatientVisitExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageCervicalCancerScreeningExtract, CervicalCancerScreeningExtract>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<StageIITRiskScore, IITRiskScore>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            //DTO >> Stage
            CreateMap<PatientSourceDto, StagePatientExtract>();
            CreateMap<PatientArtSourceDto, StageArtExtract>();
            CreateMap<PatientBaselineSourceDto, StageBaselineExtract>();
            CreateMap<StatusSourceDto, StageStatusExtract>();
            CreateMap<AdverseEventSourceDto, StageAdverseEventExtract>();
            CreateMap<PatientVisitSourceDto, StageVisitExtract>();
            CreateMap<PharmacySourceDto,StagePharmacyExtract>();
            CreateMap<LaboratorySourceDto, StageLaboratoryExtract>();
            CreateMap<AllergiesChronicIllnessSourceDto, StageAllergiesChronicIllnessExtract>();
            CreateMap<PatientIptSourceDto, StageIptExtract>();
            CreateMap<DepressionScreeningSourceDto, StageDepressionScreeningExtract>();
            CreateMap<ContactListingSourceDto, StageContactListingExtract>();
            CreateMap<GbvScreeningSourceDto, StageGbvScreeningExtract>();
            CreateMap<EnhancedAdherenceCounselingSourceDto, StageEnhancedAdherenceCounsellingExtract>();
            CreateMap<DrugAlcoholScreeningSourceDto, StageDrugAlcoholScreeningExtract>();
            CreateMap<OvcSourceDto, StageOvcExtract>();
            CreateMap<OtzSourceDto, StageOtzExtract>();
            CreateMap<CovidSourceDto, StageCovidExtract>();
            CreateMap<DefaulterTracingSourceDto, StageDefaulterTracingExtract>();
            CreateMap<CervicalCancerScreeningSourceDto, StageCervicalCancerScreeningExtract>();
            CreateMap<IndicatorItemDto, StageIITRiskScore>();
        }
    }
}
