using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Application.Interfaces.Ct;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public class StageOvcExtract : StageExtract, IOvc
    {
        public int VisitID { get ; set ; }
        public DateTime VisitDate { get ; set ; }
        public string? FacilityName { get ; set ; }
        public DateTime? OVCEnrollmentDate { get ; set ; }
        public string? RelationshipToClient { get ; set ; }
        public string? EnrolledinCPIMS { get ; set ; }
        public string? CPIMSUniqueIdentifier { get ; set ; }
        public string? PartnerOfferingOVCServices { get ; set ; }
        public string? OVCExitReason { get ; set ; }
        public DateTime? ExitDate { get ; set ; }
        public int PatientPk { get ; set ; }
        public int SiteCode { get ; set ; }
        public DateTime? DateCreated { get ; set ; }
        public DateTime? DateLastModified { get ; set ; }
        public DateTime? DateExtracted { get ; set ; }
        public DateTime? Created { get ; set ; }
        public DateTime? Updated { get ; set ; }
        public bool? Voided { get ; set ; }
    }
}
