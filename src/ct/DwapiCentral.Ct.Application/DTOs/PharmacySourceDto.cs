using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class PharmacySourceDto : IPharmacy
    {        
        public Guid Id { get; set; }
        public ulong Mhash { get; set; }
        public string RecordUUID { get; set; }
        public int? VisitID { get; set; }
        public string? Drug { get; set; }
        public string? Provider { get; set; }
        public DateTime? DispenseDate { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ExpectedReturn { get; set; }
        public string? TreatmentType { get; set; }
        public string? RegimenLine { get; set; }
        public string? PeriodTaken { get; set; }
        public string? ProphylaxisType { get; set; }
        public string? RegimenChangedSwitched { get; set; }
        public string? RegimenChangeSwitchReason { get; set; }
        public string? StopRegimenReason { get; set; }
        public DateTime? StopRegimenDate { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public PharmacySourceDto()
        {

        }

        public PharmacySourceDto(PatientPharmacyExtract patientPharmacyExtract)
        {
            VisitID = patientPharmacyExtract.VisitID;
            Drug = patientPharmacyExtract.Drug;
            Provider = patientPharmacyExtract.Provider;
            DispenseDate = patientPharmacyExtract.DispenseDate;
            Duration = patientPharmacyExtract.Duration;
            ExpectedReturn = patientPharmacyExtract.ExpectedReturn;
            TreatmentType = patientPharmacyExtract.TreatmentType;
            PeriodTaken = patientPharmacyExtract.PeriodTaken;
            RegimenLine = patientPharmacyExtract.RegimenLine;
            PeriodTaken = patientPharmacyExtract.PeriodTaken;
            ProphylaxisType = patientPharmacyExtract.ProphylaxisType;
            SiteCode = patientPharmacyExtract.SiteCode;
            PatientPk = patientPharmacyExtract.PatientPk;
           

            RegimenChangedSwitched = patientPharmacyExtract.RegimenChangedSwitched;
            RegimenChangeSwitchReason = patientPharmacyExtract.RegimenChangeSwitchReason;
            StopRegimenReason = patientPharmacyExtract.StopRegimenReason;
            StopRegimenDate = patientPharmacyExtract.StopRegimenDate;
            Date_Created = patientPharmacyExtract.Date_Created;
            Date_Last_Modified = patientPharmacyExtract.Date_Last_Modified;
            RecordUUID = patientPharmacyExtract.RecordUUID;

        }

        public IEnumerable<PharmacySourceDto> GeneratePatientPharmacyExtractDtOs(IEnumerable<PatientPharmacyExtract> extracts)
        {
            var pharmacyExtractDtos = new List<PharmacySourceDto>();
            foreach (var e in extracts.ToList())
            {
                pharmacyExtractDtos.Add(new PharmacySourceDto(e));
            }
            return pharmacyExtractDtos;
        }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}