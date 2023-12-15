using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.test;


public class Demographic
{
    public string FacilityName { get; set; }
    public string Gender { get; set; }
    public DateTime? DOB { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public DateTime? RegistrationAtCCC { get; set; }
    public DateTime? RegistrationATPMTCT { get; set; }
    public string? RegistrationAtTBClinic { get; set; }
    public string PatientSource { get; set; }
    public string Region { get; set; }
    public string District { get; set; }
    public string Village { get; set; }
    public string ContactRelation { get; set; }
    public DateTime? LastVisit { get; set; }
    public string MaritalStatus { get; set; }
    public string EducationLevel { get; set; }
    public DateTime? DateConfirmedHIVPositive { get; set; }
    public string PreviousARTExposure { get; set; }
    public string? DatePreviousARTStart { get; set; }
    public string StatusAtCCC { get; set; }
    public string? StatusAtPMTCT { get; set; }
    public string? StatusAtTBClinic { get; set; }
    public string? Orphan { get; set; }
    public string? Inschool { get; set; }
    public string PatientType { get; set; }
    public string PopulationType { get; set; }
    public string KeyPopulationType { get; set; }
    public string PatientResidentCounty { get; set; }
    public string PatientResidentSubCounty { get; set; }
    public string PatientResidentLocation { get; set; }
    public string PatientResidentSubLocation { get; set; }
    public string PatientResidentWard { get; set; }
    public string PatientResidentVillage { get; set; }
    public string? TransferInDate { get; set; }
    public DateTime? Date_Created { get; set; }
    public DateTime? Date_Last_Modified { get; set; }
    public string RecordUUID { get; set; }
    public string Pkv { get; set; }
    public string Occupation { get; set; }
    public string NUPI { get; set; }
    public int? PatientPID { get; set; }
    public string PatientCccNumber { get; set; }
    public int? FacilityId { get; set; }

    public bool? HasIIT { get; set; }
    public int? PatientPK { get; set; }
    public int? SiteCode { get; set; }
    
    public string? Processed { get; set; }
    public string? QueueId { get; set; }
    public string? Status { get; set; }
    public string? StatusDate { get; set; }
    public DateTime? DateExtracted { get; set; }
    public string Emr { get; set; }
    public string Project { get; set; }
    public bool? IsSent { get; set; }
    public string Id { get; set; }
}

public class Facility
{
    public int? Code { get; set; }
    public string Name { get; set; }
    public string Emr { get; set; }
    public string Project { get; set; }
}

public class LaboratoryExtract
{
    
    public string FacilityName { get; set; }
    public string SatelliteName { get; set; }
    public string Reason { get; set; }
    public int? VisitId { get; set; }
    public DateTime? OrderedByDate { get; set; }
    public DateTime? ReportedByDate { get; set; }
    public string TestName { get; set; }
    public string? EnrollmentTest { get; set; }
    public string TestResult { get; set; }
    public DateTime? DateSampleTaken { get; set; }
    public string SampleType { get; set; }
    public string RecordUUID { get; set; }
    public DateTime? Date_Created { get; set; }
    public DateTime? Date_Last_Modified { get; set; }
    public int? PatientPK { get; set; }
    public int? SiteCode { get; set; }
    public string PatientID { get; set; }
    public int? FacilityId { get; set; }
    public string? Processed { get; set; }
    public string? QueueId { get; set; }
    public string? Status { get; set; }
    public string? StatusDate { get; set; }
    public DateTime? DateExtracted { get; set; }
    public string Emr { get; set; }
    public string Project { get; set; }
    public bool? IsSent { get; set; }
    public string Id { get; set; }
}

public class PatientExtractView
{
    public string FacilityName { get; set; }
    public string Gender { get; set; }
    public DateTime? DOB { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public DateTime? RegistrationAtCCC { get; set; }
    public DateTime? RegistrationATPMTCT { get; set; }
    public string? RegistrationAtTBClinic { get; set; }
    public string PatientSource { get; set; }
    public string Region { get; set; }
    public string District { get; set; }
    public string Village { get; set; }
    public string ContactRelation { get; set; }
    public DateTime? LastVisit { get; set; }
    public string MaritalStatus { get; set; }
    public string EducationLevel { get; set; }
    public DateTime? DateConfirmedHIVPositive { get; set; }
    public string PreviousARTExposure { get; set; }
    public string? DatePreviousARTStart { get; set; }
    public string StatusAtCCC { get; set; }
    public string? StatusAtPMTCT { get; set; }
    public string? StatusAtTBClinic { get; set; }
    public string? Orphan { get; set; }
    public string? Inschool { get; set; }
    public string PatientType { get; set; }
    public string PopulationType { get; set; }
    public string KeyPopulationType { get; set; }
    public string PatientResidentCounty { get; set; }
    public string PatientResidentSubCounty { get; set; }
    public string PatientResidentLocation { get; set; }
    public string PatientResidentSubLocation { get; set; }
    public string PatientResidentWard { get; set; }
    public string PatientResidentVillage { get; set; }
    public string? TransferInDate { get; set; }
    public DateTime? Date_Created { get; set; }
    public DateTime? Date_Last_Modified { get; set; }
    public string RecordUUID { get; set; }
    public string Pkv { get; set; }
    public string Occupation { get; set; }
    public string NUPI { get; set; }
    public int? PatientPID { get; set; }
    public string PatientCccNumber { get; set; }
    public int? FacilityId { get; set; }

    public bool? HasIIT { get; set; }
    public int? PatientPK { get; set; }
    public int? SiteCode { get; set; }
    public string PatientID { get; set; }
    public string? Processed { get; set; }
    public string? QueueId { get; set; }
    public string? Status { get; set; }
    public string? StatusDate { get; set; }
    public DateTime? DateExtracted { get; set; }
    public string Emr { get; set; }
    public string Project { get; set; }
    public bool? IsSent { get; set; }
    public string Id { get; set; }
}



public class Root
{
    public Facility Facility { get; set; }
    public Demographic Demographic { get; set; }
    public List<LaboratoryExtract> LaboratoryExtracts { get; set; }

    
}


