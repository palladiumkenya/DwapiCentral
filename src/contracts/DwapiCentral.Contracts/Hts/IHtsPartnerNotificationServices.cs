using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Hts
{
    public interface IHtsPartnerNotificationServices : IExtract
    {
                 
        string HtsNumber { get; set; }
        int? PartnerPersonID { get; set; }
        DateTime? DateElicited { get; set; }
        string FacilityName { get; set; }       
        int? PartnerPatientPk { get; set; }          
        int? Age { get; set; }
        string? Sex { get; set; }
        string? RelationsipToIndexClient { get; set; }
        string? ScreenedForIpv { get; set; }
        string? IpvScreeningOutcome { get; set; }
        string? CurrentlyLivingWithIndexClient { get; set; }
        string? KnowledgeOfHivStatus { get; set; }
        string? PnsApproach { get; set; }
        string? PnsConsent { get; set; }
        string? LinkedToCare { get; set; }
        DateTime? LinkDateLinkedToCare { get; set; }
        string? CccNumber { get; set; }
        string? FacilityLinkedTo { get; set; }
        DateTime? Dob { get; set; }          
        string? MaritalStatus { get; set; }         
        DateTime? Date_Last_Modified { get; set; }
        int? IndexPatientPk { get; set; }
    }
}
