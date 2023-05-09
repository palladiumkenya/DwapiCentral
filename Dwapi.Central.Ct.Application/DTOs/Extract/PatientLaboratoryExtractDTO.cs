using DwapiCentral.Ct.Application.Interfaces.DTOs;
using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DwapiCentral.Ct.Application.DTOs.Extract
{
    public  class PatientLaboratoryExtractDTO : IPatientLaboratoryExtractDTO
    {
        public int? VisitId { get; set; }
        public DateTime? OrderedByDate { get; set; }
        public DateTime? ReportedByDate { get; set; }
        public string TestName { get; set; }
        public int? EnrollmentTest { get; set; }
        public string TestResult { get; set; }
        public string Emr { get; set; }
        public string Project { get; set; }
        public Guid PatientId { get; set; }
        public DateTime? DateSampleTaken { get; set; }
        public string SampleType { get; set; }
        public DateTime? Date_Created { get; set; }
        public DateTime? Date_Last_Modified { get; set; }
        public Guid Id { get; set; }
        public int PatientPk { get; set; }
        public int SiteCode { get; set; }
        public bool Voided { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Extracted { get; set; }

        public PatientLaboratoryExtractDTO()
        {
        }

        public PatientLaboratoryExtractDTO(int? visitId, DateTime? orderedByDate, DateTime? reportedByDate, string testName, int? enrollmentTest, string testResult, string emr, string project, Guid patientId, DateTime? date_Created,DateTime? date_Last_Modified)
        {
            VisitId = visitId;
            OrderedByDate = orderedByDate;
            ReportedByDate = reportedByDate;
            TestName = testName;
            EnrollmentTest = enrollmentTest;
            TestResult = testResult;
            Emr = emr;
            Project = project;
            PatientId = patientId;
            Date_Created=date_Created;
            Date_Last_Modified=date_Last_Modified;
        }


        public PatientLaboratoryExtractDTO(PatientLaboratoryExtract patientLaboratoryExtract)
        {
            VisitId = patientLaboratoryExtract.VisitId;
            OrderedByDate = patientLaboratoryExtract.OrderedByDate;
            ReportedByDate = patientLaboratoryExtract.ReportedByDate;
            TestName = patientLaboratoryExtract.TestName;
            EnrollmentTest = patientLaboratoryExtract.EnrollmentTest;
            TestResult = patientLaboratoryExtract.TestResult;
            Emr = patientLaboratoryExtract.Emr;
            Project = patientLaboratoryExtract.Project;
            PatientId = patientLaboratoryExtract.PatientId;
            DateSampleTaken = patientLaboratoryExtract.DateSampleTaken;
            SampleType = patientLaboratoryExtract.SampleType;
            Date_Created=patientLaboratoryExtract.Date_Created;
            Date_Last_Modified=patientLaboratoryExtract.Date_Last_Modified;
        }


        public IEnumerable<PatientLaboratoryExtractDTO> GenerateLaboratoryExtractDtOs(IEnumerable<PatientLaboratoryExtract> extracts)
        {
            var laboratoryExtractDtos = new List<PatientLaboratoryExtractDTO>();
            foreach (var e in extracts.ToList())
            {
                laboratoryExtractDtos.Add(new PatientLaboratoryExtractDTO(e));
            }
            return laboratoryExtractDtos;
        }

        public PatientLaboratoryExtract GeneratePatientLaboratoryExtract(Guid patientId)
        {
            PatientId = patientId;
            return new PatientLaboratoryExtract(VisitId, OrderedByDate, ReportedByDate, TestName,EnrollmentTest, TestResult, PatientId, Emr, Project,DateSampleTaken,SampleType, Date_Created, Date_Last_Modified);
        }


    }
}
