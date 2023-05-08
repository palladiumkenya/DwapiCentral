using DwapiCentral.Shared.Domain.Model.Ct;
using DwapiCentral.Shared.Application.Interfaces.Ct.Profiles;
using DwapiCentral.Shared.Domain.Model.Ct.DTOs;
using DwapiCentral.CT.Domain.Models.DTOs;

namespace DwapiCentral.Shared.Domain.Model.Ct.Profiles
{
    public class AllergiesChronicIllnessProfile : ExtractProfile<AllergiesChronicIllnessExtract>, IAllergiesChronicIllnessProfile
    {
        public List<AllergiesChronicIllnessExtractDTO> AllergiesChronicIllnessExtracts { get; set; } = new List<AllergiesChronicIllnessExtractDTO>();
        

        public static AllergiesChronicIllnessProfile Create(Facility facility, PatientExtract patient)
        {
            var patientProfile = new AllergiesChronicIllnessProfile
            {
                Facility = new FacilityDTO(facility),
                Demographic = new PatientExtractDTO(patient),
                AllergiesChronicIllnessExtracts =
                    new AllergiesChronicIllnessExtractDTO().GenerateAllergiesChronicIllnessExtractDtOs(patient.AllergiesChronicIllnessExtracts)
                        .ToList()
            };
            return patientProfile;
        }

        public static List<AllergiesChronicIllnessProfile> Create(Facility facility, List<PatientExtract> patients)
        {
            var patientProfiles = new List<AllergiesChronicIllnessProfile>();
            foreach (var patient in patients)
            {
                var patientProfile = Create(facility, patient);
                patientProfiles.Add(patientProfile);
            }

            return patientProfiles;
        }
        public override bool IsValid()
        {
            return base.IsValid() && AllergiesChronicIllnessExtracts.Count > 0;
        }

        public override bool HasData()
        {
            return base.HasData() && null != AllergiesChronicIllnessExtracts;
        }

        public override void GenerateRecords(Guid patientId)
        {
            base.GenerateRecords(patientId);
            foreach (var e in AllergiesChronicIllnessExtracts)
                Extracts.Add(e.GenerateAllergiesChronicIllnessExtract(PatientInfo.Id));
        }
    }
}
