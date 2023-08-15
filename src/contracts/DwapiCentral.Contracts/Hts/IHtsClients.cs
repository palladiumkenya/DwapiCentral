using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
      public interface IHtsClients : IExtract
    {
         string HtsNumber { get; set; }
         string FacilityName { get; set; }
         string? Serial { get; set; }
         DateTime? StatusDate { get; set; }
         int? EncounterId { get; set; }
         DateTime? VisitDate { get; set; }
         DateTime? Dob { get; set; }
         string Gender { get; set; }
         string? MaritalStatus { get; set; }
         string? KeyPopulationType { get; set; }
         string? PopulationType { get; set; }
         string? PatientDisabled { get; set; }
         string? County { get; set; }
         string? SubCounty { get; set; }
         string? Ward { get; set; }
         string? NUPI { get; set; }
         string Pkv { get; set; }
        string? Occupation { get; set; }
        string? PriorityPopulationType { get; set; }
        string? HtsRecencyId { get; set; }



    }
}
