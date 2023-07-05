using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwapiCentral.Ct.Infrastructure.Migrations
{
    public partial class InitialCt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Manifests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Docket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadMode = table.Column<int>(type: "int", nullable: false),
                    DwapiVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmrSetup = table.Column<int>(type: "int", nullable: false),
                    EmrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmrName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmrVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Session = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterFacilities",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterFacilities", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "PatientExtracts",
                columns: table => new
                {
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    CccNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nupi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MpiId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationATPMTCT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationAtTBClinic = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Village = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactRelation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviousARTExposure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusAtCCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusAtPMTCT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusAtTBClinic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orphan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inschool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentSubCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentSubLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentWard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentVillage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExtracts", x => new { x.PatientPk, x.SiteCode });
                });

            migrationBuilder.CreateTable(
                name: "StageAdverseEventExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdverseEvent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdverseEventEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventClinicalOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventIsPregnant = table.Column<bool>(type: "bit", nullable: true),
                    AdverseEventRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventCause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageAdverseEventExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageAllergiesChronicIllnessExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChronicIllness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChronicOnsetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    knownAllergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergyCausativeAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergicReaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergySeverity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergyOnsetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Skin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eyes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abdomen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genitourinary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageAllergiesChronicIllnessExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageArtExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastARTDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AgeEnrollment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AgeARTStart = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AgeLastVisit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviousARTRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartARTAtThisFacility = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartRegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpectedReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviousARTUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousARTPurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastUsed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageArtExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageBaselineExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bCD4 = table.Column<int>(type: "int", nullable: true),
                    bCD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bWAB = table.Column<int>(type: "int", nullable: true),
                    bWABDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bWHO = table.Column<int>(type: "int", nullable: true),
                    bWHODate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    eWAB = table.Column<int>(type: "int", nullable: true),
                    eWABDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    eCD4 = table.Column<int>(type: "int", nullable: true),
                    eCD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    eWHO = table.Column<int>(type: "int", nullable: true),
                    eWHODate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastWHO = table.Column<int>(type: "int", nullable: true),
                    lastWHODate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastCD4 = table.Column<int>(type: "int", nullable: true),
                    lastCD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastWAB = table.Column<int>(type: "int", nullable: true),
                    lastWABDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    m12CD4 = table.Column<int>(type: "int", nullable: true),
                    m12CD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    m6CD4 = table.Column<int>(type: "int", nullable: true),
                    m6CD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageBaselineExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageContactListingExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerPersonID = table.Column<int>(type: "int", nullable: true),
                    ContactAge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactMaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipWithPatient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenedForIpv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpvScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPVScreeningOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PnsApproach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPatientPK = table.Column<int>(type: "int", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageContactListingExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageCovidExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    Covid19AssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceivedCOVID19Vaccine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateGivenFirstDose = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstDoseVaccineAdministered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateGivenSecondDose = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondDoseVaccineAdministered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaccinationStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaccineVerification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoosterGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoosterDose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoosterDoseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EverCOVID19Positive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COVID19TestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MissedAppointmentDueToCOVID19 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COVID19PositiveSinceLasVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COVID19TestDateSinceLastVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientStatusSinceLastVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionStatusSinceLastVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdmissionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdmissionUnitSinceLastVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplementalOxygenReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientVentilated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TracingFinalOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CauseOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COVID19TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoosterDoseVerified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageCovidExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageDefaulterTracingExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    TracingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttemptNumber = table.Column<int>(type: "int", nullable: true),
                    IsFinalTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrueStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CauseOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageDefaulterTracingExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageDepressionScreeningExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ_9_rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepressionAssesmentScore = table.Column<int>(type: "int", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageDepressionScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageDrugAlcoholScreeningExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrinkingAlcohol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Smoking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrugUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageDrugAlcoholScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageEnhancedAdherenceCounsellingExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionNumber = table.Column<int>(type: "int", nullable: true),
                    DateOfFirstSession = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PillCountAdherence = table.Column<int>(type: "int", nullable: true),
                    MMAS4_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMAS4_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMAS4_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMAS4_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSA8_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSA8_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSA8_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSA8_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSAScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACRecievedVL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACVL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACVLConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACVLThoughts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACWayForward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACCognitiveBarrier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEmotionalBarriers_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEmotionalBarriers_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReviewImprovement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReviewMissedDoses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReviewStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReferral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReferralApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReferralExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACHomevisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACAdherencePlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACFollowupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageEnhancedAdherenceCounsellingExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageGbvScreeningExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalIPV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmotionalIPV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexualIPV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPVRelationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageGbvScreeningExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageIptExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnTBDrugs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverOnIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cough = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fever = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoticeableWeightLoss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NightSweats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lethargy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICFActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBClinicalDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactsInvited = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluatedForIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAntiTBs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBRxStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TBScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPTClientWorkUp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndicationForIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageIptExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageLaboratoryExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    OrderedByDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportedByDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollmentTest = table.Column<int>(type: "int", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSampleTaken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SampleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageLaboratoryExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageOtzExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OTZEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransferInStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModulesPreviouslyCovered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModulesCompletedToday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportGroupInvolvement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransitionAttritionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutcomeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageOtzExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageOvcExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OVCEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RelationshipToClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrolledinCPIMS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPIMSUniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerOfferingOVCServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OVCExitReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageOvcExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StagePatientExtracts",
                columns: table => new
                {
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    CccNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nupi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MpiId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationATPMTCT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationAtTBClinic = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Village = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactRelation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateConfirmedHIVPositive = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviousARTExposure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusAtCCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusAtPMTCT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusAtTBClinic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Orphan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inschool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentSubCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentSubLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentWard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentVillage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePatientExtracts", x => new { x.PatientPk, x.SiteCode });
                });

            migrationBuilder.CreateTable(
                name: "StagePharmacyExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    Drug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpectedReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TreatmentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProphylaxisType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenChangedSwitched = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenChangeSwitchReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StopRegimenReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StopRegimenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePharmacyExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageStatusExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExitDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExitReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TOVerified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TOVerifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReasonForDeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificDeathReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveDiscontinuationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageStatusExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageVisitExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WHOStage = table.Column<int>(type: "int", nullable: true),
                    WABStage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pregnant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EDD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OIDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubstitutionFirstlineRegimenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubstitutionFirstlineRegimenReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubstitutionSecondlineRegimenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubstitutionSecondlineRegimenReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondlineRegimenChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondlineRegimenChangeReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adherence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyPlanningMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PwP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GestationAge = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StabilityAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifferentiatedCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Muac = table.Column<int>(type: "int", nullable: true),
                    NutritionalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverHadMenses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Breastfeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Menopausal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoFPReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProphylaxisUsed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CTXAdherence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HCWConcern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCAReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralExamination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemExamination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eyes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abdomen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genitourinary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefillDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveSession = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    Generated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageVisitExtracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Metrics_Manifests_ManifestId",
                        column: x => x.ManifestId,
                        principalTable: "Manifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllergiesChronicIllnessExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChronicIllness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChronicOnsetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    knownAllergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergyCausativeAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergicReaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergySeverity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergyOnsetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Skin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eyes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abdomen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genitourinary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergiesChronicIllnessExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllergiesChronicIllnessExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contactListingExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerPersonID = table.Column<int>(type: "int", nullable: true),
                    ContactAge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactMaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipWithPatient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenedForIpv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpvScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPVScreeningOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PnsApproach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPatientPK = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactListingExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contactListingExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CovidExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    Covid19AssessmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedCOVID19Vaccine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateGivenFirstDose = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstDoseVaccineAdministered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateGivenSecondDose = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondDoseVaccineAdministered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaccinationStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaccineVerification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoosterGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoosterDose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoosterDoseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EverCOVID19Positive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COVID19TestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MissedAppointmentDueToCOVID19 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COVID19PositiveSinceLasVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COVID19TestDateSinceLastVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientStatusSinceLastVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionStatusSinceLastVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdmissionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdmissionUnitSinceLastVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplementalOxygenReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientVentilated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TracingFinalOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CauseOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COVID19TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sequence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoosterDoseVerified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CovidExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CovidExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DefaulterTracingExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    TracingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttemptNumber = table.Column<int>(type: "int", nullable: true),
                    IsFinalTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrueStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CauseOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaulterTracingExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefaulterTracingExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepressionScreeningExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ9_9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHQ_9_rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepressionAssesmentScore = table.Column<int>(type: "int", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepressionScreeningExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepressionScreeningExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrugAlcoholScreeningExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrinkingAlcohol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Smoking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrugUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugAlcoholScreeningExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrugAlcoholScreeningExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnhancedAdherenceCounsellingExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SessionNumber = table.Column<int>(type: "int", nullable: true),
                    DateOfFirstSession = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PillCountAdherence = table.Column<int>(type: "int", nullable: true),
                    MMAS4_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMAS4_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMAS4_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMAS4_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSA8_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSA8_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSA8_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSA8_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MMSAScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACRecievedVL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACVL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACVLConcerns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACVLThoughts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACWayForward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACCognitiveBarrier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACBehaviouralBarrier_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEmotionalBarriers_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEmotionalBarriers_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACEconBarrier_8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReviewImprovement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReviewMissedDoses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReviewStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReferral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReferralApp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACReferralExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACHomevisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACAdherencePlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EACFollowupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnhancedAdherenceCounsellingExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnhancedAdherenceCounsellingExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GbvScreeningExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalIPV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmotionalIPV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexualIPV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPVRelationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GbvScreeningExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GbvScreeningExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IptExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnTBDrugs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverOnIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cough = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fever = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoticeableWeightLoss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NightSweats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lethargy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICFActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBClinicalDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactsInvited = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvaluatedForIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAntiTBs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBRxStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TBScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPTClientWorkUp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndicationForIPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IptExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IptExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtzExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTZEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransferInStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModulesPreviouslyCovered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModulesCompletedToday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupportGroupInvolvement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransitionAttritionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutcomeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtzExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtzExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OvcExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OVCEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RelationshipToClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrolledinCPIMS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPIMSUniqueIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerOfferingOVCServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OVCExitReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvcExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OvcExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAdverseEventExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdverseEvent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdverseEventEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventClinicalOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventIsPregnant = table.Column<bool>(type: "bit", nullable: true),
                    AdverseEventRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventCause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAdverseEventExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAdverseEventExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientArtExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    LastARTDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AgeEnrollment = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AgeARTStart = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AgeLastVisit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviousARTStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviousARTRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartARTAtThisFacility = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartRegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpectedReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreviousARTUse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousARTPurpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastUsed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientArtExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientArtExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientBaselinesExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    bCD4 = table.Column<int>(type: "int", nullable: true),
                    bCD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bWAB = table.Column<int>(type: "int", nullable: true),
                    bWABDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bWHO = table.Column<int>(type: "int", nullable: true),
                    bWHODate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    eWAB = table.Column<int>(type: "int", nullable: true),
                    eWABDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    eCD4 = table.Column<int>(type: "int", nullable: true),
                    eCD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    eWHO = table.Column<int>(type: "int", nullable: true),
                    eWHODate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastWHO = table.Column<int>(type: "int", nullable: true),
                    lastWHODate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastCD4 = table.Column<int>(type: "int", nullable: true),
                    lastCD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lastWAB = table.Column<int>(type: "int", nullable: true),
                    lastWABDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    m12CD4 = table.Column<int>(type: "int", nullable: true),
                    m12CD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    m6CD4 = table.Column<int>(type: "int", nullable: true),
                    m6CD4Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientBaselinesExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientBaselinesExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientLaboratoryExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    OrderedByDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportedByDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollmentTest = table.Column<int>(type: "int", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateSampleTaken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SampleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientLaboratoryExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientLaboratoryExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientPharmacyExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    DispenseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Drug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ExpectedReturn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TreatmentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PeriodTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProphylaxisType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenChangedSwitched = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenChangeSwitchReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StopRegimenReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StopRegimenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPharmacyExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPharmacyExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientStatusExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TOVerifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TOVerified = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReasonForDeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificDeathReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EffectiveDiscontinuationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientStatusExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientStatusExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientVisitExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WHOStage = table.Column<int>(type: "int", nullable: true),
                    WABStage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pregnant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EDD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OIDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubstitutionFirstlineRegimenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubstitutionFirstlineRegimenReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubstitutionSecondlineRegimenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubstitutionSecondlineRegimenReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondlineRegimenChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondlineRegimenChangeReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adherence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyPlanningMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PwP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GestationAge = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StabilityAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifferentiatedCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Muac = table.Column<int>(type: "int", nullable: true),
                    NutritionalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverHadMenses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Breastfeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Menopausal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoFPReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProphylaxisUsed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CTXAdherence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HCWConcern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TCAReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralExamination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SystemExamination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Eyes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abdomen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CNS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genitourinary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefillDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVisitExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVisitExtracts_PatientExtracts_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PatientExtracts",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergiesChronicIllnessExtracts_PatientPk_SiteCode",
                table: "AllergiesChronicIllnessExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_contactListingExtracts_PatientPk_SiteCode",
                table: "contactListingExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_CovidExtracts_PatientPk_SiteCode",
                table: "CovidExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_DefaulterTracingExtracts_PatientPk_SiteCode",
                table: "DefaulterTracingExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_DepressionScreeningExtracts_PatientPk_SiteCode",
                table: "DepressionScreeningExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_DrugAlcoholScreeningExtracts_PatientPk_SiteCode",
                table: "DrugAlcoholScreeningExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_EnhancedAdherenceCounsellingExtracts_PatientPk_SiteCode",
                table: "EnhancedAdherenceCounsellingExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_GbvScreeningExtracts_PatientPk_SiteCode",
                table: "GbvScreeningExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_IptExtracts_PatientPk_SiteCode",
                table: "IptExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ManifestId",
                table: "Metrics",
                column: "ManifestId");

            migrationBuilder.CreateIndex(
                name: "IX_OtzExtracts_PatientPk_SiteCode",
                table: "OtzExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_OvcExtracts_PatientPk_SiteCode",
                table: "OvcExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientAdverseEventExtracts_PatientPk_SiteCode",
                table: "PatientAdverseEventExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientArtExtracts_PatientPk_SiteCode",
                table: "PatientArtExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientBaselinesExtracts_PatientPk_SiteCode",
                table: "PatientBaselinesExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientLaboratoryExtracts_PatientPk_SiteCode",
                table: "PatientLaboratoryExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientPharmacyExtracts_PatientPk_SiteCode",
                table: "PatientPharmacyExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientStatusExtracts_PatientPk_SiteCode",
                table: "PatientStatusExtracts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisitExtracts_PatientPk_SiteCode",
                table: "PatientVisitExtracts",
                columns: new[] { "PatientPk", "SiteCode" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergiesChronicIllnessExtracts");

            migrationBuilder.DropTable(
                name: "contactListingExtracts");

            migrationBuilder.DropTable(
                name: "CovidExtracts");

            migrationBuilder.DropTable(
                name: "DefaulterTracingExtracts");

            migrationBuilder.DropTable(
                name: "DepressionScreeningExtracts");

            migrationBuilder.DropTable(
                name: "DrugAlcoholScreeningExtracts");

            migrationBuilder.DropTable(
                name: "EnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "GbvScreeningExtracts");

            migrationBuilder.DropTable(
                name: "IptExtracts");

            migrationBuilder.DropTable(
                name: "MasterFacilities");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropTable(
                name: "OtzExtracts");

            migrationBuilder.DropTable(
                name: "OvcExtracts");

            migrationBuilder.DropTable(
                name: "PatientAdverseEventExtracts");

            migrationBuilder.DropTable(
                name: "PatientArtExtracts");

            migrationBuilder.DropTable(
                name: "PatientBaselinesExtracts");

            migrationBuilder.DropTable(
                name: "PatientLaboratoryExtracts");

            migrationBuilder.DropTable(
                name: "PatientPharmacyExtracts");

            migrationBuilder.DropTable(
                name: "PatientStatusExtracts");

            migrationBuilder.DropTable(
                name: "PatientVisitExtracts");

            migrationBuilder.DropTable(
                name: "StageAdverseEventExtracts");

            migrationBuilder.DropTable(
                name: "StageAllergiesChronicIllnessExtracts");

            migrationBuilder.DropTable(
                name: "StageArtExtracts");

            migrationBuilder.DropTable(
                name: "StageBaselineExtracts");

            migrationBuilder.DropTable(
                name: "StageContactListingExtracts");

            migrationBuilder.DropTable(
                name: "StageCovidExtracts");

            migrationBuilder.DropTable(
                name: "StageDefaulterTracingExtracts");

            migrationBuilder.DropTable(
                name: "StageDepressionScreeningExtracts");

            migrationBuilder.DropTable(
                name: "StageDrugAlcoholScreeningExtracts");

            migrationBuilder.DropTable(
                name: "StageEnhancedAdherenceCounsellingExtracts");

            migrationBuilder.DropTable(
                name: "StageGbvScreeningExtracts");

            migrationBuilder.DropTable(
                name: "StageIptExtracts");

            migrationBuilder.DropTable(
                name: "StageLaboratoryExtracts");

            migrationBuilder.DropTable(
                name: "StageOtzExtracts");

            migrationBuilder.DropTable(
                name: "StageOvcExtracts");

            migrationBuilder.DropTable(
                name: "StagePatientExtracts");

            migrationBuilder.DropTable(
                name: "StagePharmacyExtracts");

            migrationBuilder.DropTable(
                name: "StageStatusExtracts");

            migrationBuilder.DropTable(
                name: "StageVisitExtracts");

            migrationBuilder.DropTable(
                name: "Manifests");

            migrationBuilder.DropTable(
                name: "PatientExtracts");
        }
    }
}
