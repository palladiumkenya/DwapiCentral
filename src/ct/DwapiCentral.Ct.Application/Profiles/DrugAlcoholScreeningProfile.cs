using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.Profiles
{
    public class DrugAlcoholScreeningProfile : ExtractProfile<DrugAlcoholScreeningExtract>, IDrugAlcoholScreeningProfile
    {
        public List<DrugAlcoholScreeningSourceDto> DrugAlcoholScreeningExtracts { get; set; } = new List<DrugAlcoholScreeningSourceDto>();

        public static DrugAlcoholScreeningProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new DrugAlcoholScreeningProfile
            {
                Facility = new FacilityDTO(facility),
                //Demographic = new PatientExtractDTO(patient),
                DrugAlcoholScreeningExtracts =
                    new DrugAlcoholScreeningSourceDto().GenerateDrugAlcoholScreeningExtractDtOs(patient.DrugAlcoholScreeningExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<DrugAlcoholScreeningProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<DrugAlcoholScreeningProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && DrugAlcoholScreeningExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != DrugAlcoholScreeningExtracts;
        }

        
    }
}
