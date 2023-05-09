using DwapiCentral.Contracts.Ct;
using DwapiCentral.Shared.Domain.Entities.Ct;

namespace DwapiCentral.Ct.Domain.Models.Extracts
{
    public class PatientLaboratoryExtract : Entity, ILab
    {
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? DateSampleTaken { get; set; }
        public string SampleType { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }

        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public bool Voided { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }

        public PatientLaboratoryExtract()
        {
            Created = DateTime.Now;
        }

        public PatientLaboratoryExtract(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, int? enrollmentTest, string testResult, Guid patientId, string emr, string project,
            DateTime? dateSampleTaken, string sampleType, DateTime? date_Created, DateTime? date_Last_Modified
            )
        {
            VisitId = visitId;
            OrderedByDate = orderedByDate;
            ReportedByDate = reportedByDate;
            TestName = testName;
            EnrollmentTest = enrollmentTest;
            TestResult = testResult;
            PatientId = patientId;
            Emr = emr;
            Project = project;
            Created = DateTime.Now;

            DateSampleTaken = dateSampleTaken;
            SampleType = sampleType;
            Date_Created = date_Created;
            Date_Last_Modified = date_Last_Modified;
        }
    }
}
