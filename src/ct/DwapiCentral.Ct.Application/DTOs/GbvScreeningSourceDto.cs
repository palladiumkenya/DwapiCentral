using DwapiCentral.Contracts.Ct;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class GbvScreeningSourceDto : SourceDto, IGbvScreening
    {
        public string FacilityName { get; set; }
        public int? VisitID { get; set; }
        public DateTime? VisitDate { get; set; }
        public string IPV { get; set; }
        public string PhysicalIPV { get; set; }
        public string EmotionalIPV { get; set; }
        public string SexualIPV { get; set; }
        public string IPVRelationship { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public int PatientPk { get; set; }
        public bool Voided { get; set; }
        public DateTime? Extracted { get; set; }
        Guid IGbvScreening.PatientId { get; set; }
    }
}
