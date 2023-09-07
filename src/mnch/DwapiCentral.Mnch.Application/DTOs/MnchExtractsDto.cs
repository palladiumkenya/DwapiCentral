using DwapiCentral.Mnch.Domain.Model;
using System.Collections.Generic;


namespace DwapiCentral.Mnch.Application.DTOs
{
    public class MnchExtractsDto
    {
        public List<PncVisit> PncVisitExtracts { get; set; }
        public List<PatientMnchExtract> PatientMnchExtracts { get; set; } 
        public List<MotherBabyPair> MotherBabyPairExtracts { get; set; } 
        public List<MnchEnrolment> MnchEnrolmentExtracts { get; set; }
        public List<MnchArt> MnchArtExtracts { get; set; }
        public List<MatVisit> MatVisitExtracts { get; set; } 
        public List<HeiExtract> HeiExtracts { get; set; }
        public List<CwcVisit> CwcVisitExtracts { get; set; } 
        public List<CwcEnrolment> CwcEnrolmentExtracts { get; set; } 
        public List<AncVisit> AncVisitExtracts { get; set; }
        public List<MnchLab> MnchLabExtracts { get; set; } 
        public List<MnchImmunization> MnchImmunizationExtracts { get; set; } 

    }
}
