﻿using DwapiCentral.Contracts.Prep;
using DwapiCentral.Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Prep.Domain.Models.Stage
{
    public class StagePatientPrep : IPatientPrep
    {
        
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string RecordUUID { get; set; }
        public string PrepNumber { get; set; }
        public string? FacilityName { get; set; }
        public string? HtsNumber { get; set; }
        public DateTime? PrepEnrollmentDate { get; set; }
        public string? Sex { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? CountyofBirth { get; set; }
        public string? County { get; set; }
        public string? SubCounty { get; set; }
        public string? Location { get; set; }
        public string? LandMark { get; set; }
        public string? Ward { get; set; }
        public string? ClientType { get; set; }
        public string? ReferralPoint { get; set; }
        public string? MaritalStatus { get; set; }
        public string? Inschool { get; set; }
        public string? PopulationType { get; set; }
        public string? KeyPopulationType { get; set; }
        public string? Refferedfrom { get; set; }
        public string? TransferIn { get; set; }
        public DateTime? TransferInDate { get; set; }
        public string? TransferFromFacility { get; set; }
        public DateTime? DatefirstinitiatedinPrepCare { get; set; }
        public DateTime? DateStartedPrEPattransferringfacility { get; set; }
        public string? ClientPreviouslyonPrep { get; set; }
        public string? PrevPrepReg { get; set; }
        public DateTime? DateLastUsedPrev { get; set; }
        public string? NUPI { get; set; }       
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public LiveStage LiveStage { get; set; }
        public Guid? ManifestId { get; set; }
    }
}
