using DwapiCentral.Contracts.Common;
using DwapiCentral.Contracts.Hts;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Hts.Domain.Model.Stage
{
    public class StageHtsClient : IHtsClients
    {
       
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string? HtsNumber { get; set; }
        public string Pkv { get; set; }
        public string? Occupation { get; set; }
        public string? PriorityPopulationType { get; set; }
        public string? HtsRecencyId { get; set; }        
        public string? FacilityName { get; set; }
        public string? Serial { get; set; }
        public DateTime? StatusDate { get; set; }
        public int? EncounterId { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string? MaritalStatus { get; set; }
        public string? KeyPopulationType { get; set; }
        public string? PopulationType { get; set; }
        public string? PatientDisabled { get; set; }
        public string? County { get; set; }
        public string? SubCounty { get; set; }
        public string? Ward { get; set; }
        public string? NUPI { get; set; }
        
        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }

        public DateTime? Date_Created { get; set; }
        public bool? Voided { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
    }
}
