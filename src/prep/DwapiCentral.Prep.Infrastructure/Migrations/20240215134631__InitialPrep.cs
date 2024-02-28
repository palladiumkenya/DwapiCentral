using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwapiCentral.Prep.Infrastructure.Migrations
{
    public partial class _InitialPrep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dockets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dockets", x => x.Id);
                });

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
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recieved = table.Column<int>(type: "int", nullable: false),
                    DateLogged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateArrived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmrName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmrSetup = table.Column<int>(type: "int", nullable: false),
                    Session = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Docket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DwapiVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmrVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "PrepPatients",
                columns: table => new
                {
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrepEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CountyofBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inschool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferedfrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransferFromFacility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatefirstinitiatedinPrepCare = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateStartedPrEPattransferringfacility = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientPreviouslyonPrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrevPrepReg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastUsedPrev = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NUPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepPatients", x => new { x.PatientPk, x.SiteCode });
                });

            migrationBuilder.CreateTable(
                name: "StagePrepAdverseEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEvent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdverseEventEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdverseEventActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventClinicalOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventIsPregnant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventCause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePrepAdverseEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StagePrepBehaviourRisks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    SexPartnerHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHIVPositivePartnerCurrentonART = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPartnerHighrisk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerARTRisk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientAssessments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientRisk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientWillingToTakePrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrEPDeclineReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskReductionEducationOffered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralToOtherPrevServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstEstablishPartnerStatus = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartnerEnrolledtoCCC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HIVPartnerCCCnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVPartnerARTStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MonthsknownHIVSerodiscordant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexWithoutCondom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberofchildrenWithPartner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePrepBehaviourRisks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StagePrepCareTerminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfLastPrepDose = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePrepCareTerminations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StagePrepLabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePrepLabs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StagePrepMonthlyRefills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BehaviorRiskAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexPartnerHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SymptomsAcuteHIV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceCounsellingDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContraIndicationForPrEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrescribedPrepToday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfMonths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondomsIssued = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfCondomsIssued = table.Column<int>(type: "int", nullable: true),
                    ClientGivenNextAppointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReasonForFailureToGiveAppointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfLastPrepDose = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePrepMonthlyRefills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StagePrepPatients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrepEnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CountyofBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandMark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inschool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferedfrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TransferFromFacility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatefirstinitiatedinPrepCare = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateStartedPrEPattransferringfacility = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientPreviouslyonPrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrevPrepReg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateLastUsedPrev = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NUPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePrepPatients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StagePrepPharmacys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    RegimenPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispenseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePrepPharmacys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StagePrepVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncounterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BMI = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    STIScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STISymptoms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STITreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Circumcised = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VMMCReferral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MenopausalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PregnantAtThisVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EDD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanningToGetPregnant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PregnancyPlanned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PregnancyEnded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PregnancyEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PregnancyOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDefects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Breastfeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyPlanningStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FPMethods = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceReasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SymptomsAcuteHIV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContraindicationsPrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrepTreatmentPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrepPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthsPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondomsIssued = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tobegivennextappointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reasonfornotgivingnextappointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HepatitisBPositiveResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HepatitisCPositiveResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaccinationForHepBStarted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatedForHepB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaccinationForHepCStarted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatedForHepC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextAppointment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StagePrepVisits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribers_Dockets_DocketId",
                        column: x => x.DocketId,
                        principalTable: "Dockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargoes_Manifests_ManifestId",
                        column: x => x.ManifestId,
                        principalTable: "Manifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepAdverseEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEvent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdverseEventEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdverseEventActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventClinicalOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventIsPregnant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventCause = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdverseEventRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepAdverseEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepAdverseEvents_PrepPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PrepPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepBehaviourRisks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    SexPartnerHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHIVPositivePartnerCurrentonART = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPartnerHighrisk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerARTRisk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientAssessments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientRisk = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientWillingToTakePrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrEPDeclineReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskReductionEducationOffered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralToOtherPrevServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstEstablishPartnerStatus = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartnerEnrolledtoCCC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HIVPartnerCCCnumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVPartnerARTStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MonthsknownHIVSerodiscordant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexWithoutCondom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberofchildrenWithPartner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepBehaviourRisks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepBehaviourRisks_PrepPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PrepPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepCareTerminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExitReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfLastPrepDose = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepCareTerminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepCareTerminations_PrepPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PrepPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepLabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SampleDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestResultDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepLabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepLabs_PrepPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PrepPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepMonthlyRefills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    RegimenPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispenseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    VisitDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BehaviorRiskAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexPartnerHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SymptomsAcuteHIV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceCounsellingDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContraIndicationForPrEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrescribedPrepToday = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfMonths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondomsIssued = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfCondomsIssued = table.Column<int>(type: "int", nullable: true),
                    ClientGivenNextAppointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReasonForFailureToGiveAppointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfLastPrepDose = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepMonthlyRefills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepMonthlyRefills_PrepPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PrepPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepPharmacys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    RegimenPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispenseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepPharmacys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepPharmacys_PrepPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PrepPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrepVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrepNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncounterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BMI = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    STIScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STISymptoms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STITreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Circumcised = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VMMCReferral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MenopausalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PregnantAtThisVisit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EDD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlanningToGetPregnant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PregnancyPlanned = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PregnancyEnded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PregnancyEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PregnancyOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDefects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Breastfeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyPlanningStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FPMethods = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdherenceReasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SymptomsAcuteHIV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContraindicationsPrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrepTreatmentPlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrepPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegimenPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthsPrescribed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondomsIssued = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tobegivennextappointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reasonfornotgivingnextappointment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HepatitisBPositiveResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HepatitisCPositiveResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaccinationForHepBStarted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatedForHepB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaccinationForHepCStarted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatedForHepC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextAppointment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrepVisits_PrepPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "PrepPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_ManifestId",
                table: "Cargoes",
                column: "ManifestId");

            migrationBuilder.CreateIndex(
                name: "IX_PrepAdverseEvents_PatientPk_SiteCode",
                table: "PrepAdverseEvents",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepBehaviourRisks_PatientPk_SiteCode",
                table: "PrepBehaviourRisks",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepCareTerminations_PatientPk_SiteCode",
                table: "PrepCareTerminations",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepLabs_PatientPk_SiteCode",
                table: "PrepLabs",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepMonthlyRefills_PatientPk_SiteCode",
                table: "PrepMonthlyRefills",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepPharmacys_PatientPk_SiteCode",
                table: "PrepPharmacys",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PrepVisits_PatientPk_SiteCode",
                table: "PrepVisits",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_DocketId",
                table: "Subscribers",
                column: "DocketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoes");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "MasterFacilities");

            migrationBuilder.DropTable(
                name: "PrepAdverseEvents");

            migrationBuilder.DropTable(
                name: "PrepBehaviourRisks");

            migrationBuilder.DropTable(
                name: "PrepCareTerminations");

            migrationBuilder.DropTable(
                name: "PrepLabs");

            migrationBuilder.DropTable(
                name: "PrepMonthlyRefills");

            migrationBuilder.DropTable(
                name: "PrepPharmacys");

            migrationBuilder.DropTable(
                name: "PrepVisits");

            migrationBuilder.DropTable(
                name: "StagePrepAdverseEvents");

            migrationBuilder.DropTable(
                name: "StagePrepBehaviourRisks");

            migrationBuilder.DropTable(
                name: "StagePrepCareTerminations");

            migrationBuilder.DropTable(
                name: "StagePrepLabs");

            migrationBuilder.DropTable(
                name: "StagePrepMonthlyRefills");

            migrationBuilder.DropTable(
                name: "StagePrepPatients");

            migrationBuilder.DropTable(
                name: "StagePrepPharmacys");

            migrationBuilder.DropTable(
                name: "StagePrepVisits");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Manifests");

            migrationBuilder.DropTable(
                name: "PrepPatients");

            migrationBuilder.DropTable(
                name: "Dockets");
        }
    }
}
