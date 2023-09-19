using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.DTOs
{
    public class AllergiesChronicIllnessSourceDto : IAllergiesChronicIllness
    {
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public string? FacilityName { get; set; }
        public string? ChronicIllness { get; set; }
        public DateTime? ChronicOnsetDate { get; set; }
        public string? knownAllergies { get; set; }
        public string? AllergyCausativeAgent { get; set; }
        public string? AllergicReaction { get; set; }
        public string? AllergySeverity { get; set; }
        public DateTime? AllergyOnsetDate { get; set; }
        public string? Skin { get; set; }
        public string? Eyes { get; set; }
        public string? ENT { get; set; }
        public string? Chest { get; set; }
        public string? CVS { get; set; }
        public string? Abdomen { get; set; }
        public string? CNS { get; set; }
        public string? Genitourinary { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public AllergiesChronicIllnessSourceDto()
        {
        }


        public AllergiesChronicIllnessSourceDto(AllergiesChronicIllnessExtract AllergiesChronicIllnessExtract)
        {
            FacilityName = AllergiesChronicIllnessExtract.FacilityName;
            VisitID = AllergiesChronicIllnessExtract.VisitID;
            VisitDate = AllergiesChronicIllnessExtract.VisitDate;
            ChronicIllness = AllergiesChronicIllnessExtract.ChronicIllness;
            ChronicOnsetDate = AllergiesChronicIllnessExtract.ChronicOnsetDate;
            knownAllergies = AllergiesChronicIllnessExtract.knownAllergies;
            AllergyCausativeAgent = AllergiesChronicIllnessExtract.AllergyCausativeAgent;
            AllergicReaction = AllergiesChronicIllnessExtract.AllergicReaction;
            AllergySeverity = AllergiesChronicIllnessExtract.AllergySeverity;
            AllergyOnsetDate = AllergiesChronicIllnessExtract.AllergyOnsetDate;
            Skin = AllergiesChronicIllnessExtract.Skin;
            Eyes = AllergiesChronicIllnessExtract.Eyes;
            ENT = AllergiesChronicIllnessExtract.ENT;
            Chest = AllergiesChronicIllnessExtract.Chest;
            CVS = AllergiesChronicIllnessExtract.CVS;
            Abdomen = AllergiesChronicIllnessExtract.Abdomen;
            CNS = AllergiesChronicIllnessExtract.CNS;
            Genitourinary = AllergiesChronicIllnessExtract.Genitourinary;
           
            Date_Created = AllergiesChronicIllnessExtract.Date_Created;
            Date_Last_Modified = AllergiesChronicIllnessExtract.Date_Last_Modified;
            RecordUUID = AllergiesChronicIllnessExtract.RecordUUID;


        }

        public IEnumerable<AllergiesChronicIllnessSourceDto> GenerateAllergiesChronicIllnessExtractDtOs(IEnumerable<AllergiesChronicIllnessExtract> extracts)
        {
            var statusExtractDtos = new List<AllergiesChronicIllnessSourceDto>();
            foreach (var e in extracts.ToList())
            {
                statusExtractDtos.Add(new AllergiesChronicIllnessSourceDto(e));
            }
            return statusExtractDtos;
        }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}
