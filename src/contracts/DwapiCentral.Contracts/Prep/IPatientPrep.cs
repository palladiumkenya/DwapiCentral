using DwapiCentral.Contracts.Common;
using System;

namespace DwapiCentral.Contracts.Prep
{
    public interface IPatientPrep : IEntity
    {
        string FacilityName { get; set; }
        string PrepNumber { get; set; }
        string HtsNumber { get; set; }
        DateTime? PrepEnrollmentDate { get; set; }
        string Sex { get; set; }
        DateTime? DateofBirth { get; set; }
        string CountyofBirth { get; set; }
        string County { get; set; }
        string SubCounty { get; set; }
        string Location { get; set; }
        string LandMark { get; set; }
        string Ward { get; set; }
        string ClientType { get; set; }
        string ReferralPoint { get; set; }
        string MaritalStatus { get; set; }
        string Inschool { get; set; }
        string PopulationType { get; set; }
        string KeyPopulationType { get; set; }
        string Refferedfrom { get; set; }
        string TransferIn { get; set; }
        DateTime? TransferInDate { get; set; }
        string TransferFromFacility { get; set; }
        DateTime? DatefirstinitiatedinPrepCare { get; set; }
        DateTime? DateStartedPrEPattransferringfacility { get; set; }
        string ClientPreviouslyonPrep { get; set; }
        string PrevPrepReg { get; set; }
        DateTime? DateLastUsedPrev { get; set; }       
        string NUPI { get; set; }
    }
}
