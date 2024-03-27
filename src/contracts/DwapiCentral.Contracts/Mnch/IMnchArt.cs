using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Mnch
{
    public  interface IMnchArt 
    {
        int PatientPk { get; set; }
        int SiteCode { get; set; }
        string? RecordUUID { get; set; }
        string? Pkv { get; set; }
        string? PatientMnchID { get; set; }
        string? PatientHeiID { get; set; }
        string? FacilityName { get; set; }
        DateTime? RegistrationAtCCC { get; set; }
        DateTime? StartARTDate { get; set; }
        string? StartRegimen { get; set; }
        string? StartRegimenLine { get; set; }
        string? StatusAtCCC { get; set; }
        DateTime? LastARTDate { get; set; }
        string? LastRegimen { get; set; }
        string? LastRegimenLine { get; set; }
        string? FacilityReceivingARTCare { get; set; }


        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }

        DateTime? DateLastModified { get; set; }
        DateTime? DateExtracted { get; set; }
        DateTime? Created { get; set; }
        DateTime? Updated { get; set; }
        bool? Voided { get; set; }

    }
}
