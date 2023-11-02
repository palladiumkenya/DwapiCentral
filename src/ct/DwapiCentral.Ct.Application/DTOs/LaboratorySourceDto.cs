using DwapiCentral.Contracts.Ct;
using DwapiCentral.Ct.Domain.Custom;
using DwapiCentral.Ct.Domain.Models;
using System;


namespace DwapiCentral.Ct.Application.DTOs
{
    public class LaboratorySourceDto : ILab
    {
        public Guid Id { get; set; }
        public string RecordUUID { get; set; }
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string? TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string? TestResult { get; set; }
        public DateTime? DateSampleTaken { get; set; }
        public string? SampleType { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string? Reason { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public DateTime? DateLastModified { get; set; }
        public DateTime? DateExtracted { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool? Voided { get; set; }

        public LaboratorySourceDto()
        {

        }

        public LaboratorySourceDto(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, int? enrollmentTest, string testResult,  int sitecode, int patientpk,
           DateTime? dateSampleTaken, string sampleType, DateTime? date_Created, DateTime? date_Last_Modified, string recordUUID
           )
        {
            VisitId = visitId;
            OrderedByDate = orderedByDate;
            ReportedByDate = reportedByDate;
            TestName = testName;
            EnrollmentTest = enrollmentTest;
            TestResult = testResult;
           
            SiteCode = sitecode;
            PatientPk = patientpk;
            Created = DateTime.Now;
            RecordUUID = recordUUID;


            DateSampleTaken = dateSampleTaken;
            SampleType = sampleType;
          
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
            this.StandardizeExtract();
        }

        public LaboratorySourceDto GeneratePatientLaboratoryExtract(string recordUUID)
        {
            RecordUUID = recordUUID;
            return new LaboratorySourceDto(VisitId, OrderedByDate, ReportedByDate, TestName, EnrollmentTest, TestResult,  SiteCode, PatientPk, DateSampleTaken, SampleType, Date_Created, Date_Last_Modified, RecordUUID);
        }

        public LaboratorySourceDto(PatientLaboratoryExtract patientLaboratoryExtract)
        {
            VisitId = patientLaboratoryExtract.VisitId;
            OrderedByDate = patientLaboratoryExtract.OrderedByDate;
            ReportedByDate = patientLaboratoryExtract.ReportedByDate;
            TestName = patientLaboratoryExtract.TestName;
            EnrollmentTest = patientLaboratoryExtract.EnrollmentTest;
            TestResult = patientLaboratoryExtract.TestResult;
            SiteCode = patientLaboratoryExtract.SiteCode;
            PatientPk = patientLaboratoryExtract.PatientPk;
           
            DateSampleTaken = patientLaboratoryExtract.DateSampleTaken;
         
            SampleType = patientLaboratoryExtract.SampleType;
            Date_Created = patientLaboratoryExtract.Date_Created;
            Date_Last_Modified = patientLaboratoryExtract.Date_Last_Modified;
            RecordUUID = patientLaboratoryExtract.RecordUUID;

        }

  


        public IEnumerable<LaboratorySourceDto> GenerateLaboratoryExtractDtOs(IEnumerable<PatientLaboratoryExtract> extracts)
        {
            var laboratoryExtractDtos = new List<LaboratorySourceDto>();
            foreach (var e in extracts.ToList())
            {
                laboratoryExtractDtos.Add(new LaboratorySourceDto(e));
            }
            return laboratoryExtractDtos;
        }

        public virtual bool IsValid()
        {
            return SiteCode > 0 &&
                   PatientPk > 0;
        }
    }
}