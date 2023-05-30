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
            
        }
    }
}
