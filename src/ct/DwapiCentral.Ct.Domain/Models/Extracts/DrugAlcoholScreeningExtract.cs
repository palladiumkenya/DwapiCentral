using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class DrugAlcoholScreeningExtract : Entity, IDrugAlcoholScreening
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string DrinkingAlcohol { get; set; }
        public string Smoking { get; set; }
        public string DrugUse { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool Voided { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }

        public DrugAlcoholScreeningExtract()
        {
            Created = DateTime.Now;
        }

        public DrugAlcoholScreeningExtract(string facilityName, int? visitId, DateTime? visitDate, string drinkingAlcohol, string smoking, string drugUse,
            Guid patientId, string emr, string project, DateTime? date_Created, DateTime? date_Last_Modified)
        {
            FacilityName = facilityName;
            VisitID = visitId;
            VisitDate = visitDate;
            DrinkingAlcohol = drinkingAlcohol;
            Smoking = smoking;
            DrugUse = drugUse;

            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
        }
    }
}
