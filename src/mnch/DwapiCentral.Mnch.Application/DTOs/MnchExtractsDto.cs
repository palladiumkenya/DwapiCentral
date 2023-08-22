using DwapiCentral.Mnch.Domain.Model;
using System.Collections.Generic;


namespace DwapiCentral.Mnch.Application.DTOs
{
    public class MnchExtractsDto
    {
        public List<PncVisit> PncVisitExtracts { get; set; } = new List<PncVisit>();
        public List<PatientMnchExtract> PatientMnchExtracts { get; set; } = new List<PatientMnchExtract>();
        public List<MotherBabyPair> MotherBabyPairExtracts { get; set; } = new List<MotherBabyPair>();
        public List<MnchEnrolment> MnchEnrolmentExtracts { get; set; } = new List<MnchEnrolment>();
        public List<MnchArt> MnchArtExtracts { get; set; } = new List<MnchArt>();
        public List<MatVisit> MatVisitExtracts { get; set; } = new List<MatVisit>();
        public List<HeiExtract> HeiExtracts { get; set; } = new List<HeiExtract>();
        public List<CwcVisit> CwcVisitExtracts { get; set; } = new List<CwcVisit>();
        public List<CwcEnrolment> CwcEnrolmentExtracts { get; set; } = new List<CwcEnrolment>();
        public List<AncVisit> AncVisitExtracts { get; set; } = new List<AncVisit>();
        public List<MnchLab> MnchLabExtracts { get; set; } = new List<MnchLab>();
        public List<MnchImmunization> MnchImmunizationExtracts { get; set; } = new List<MnchImmunization>();

    }
}
