using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwapiCentral.Hts.Infrastructure.Migrations
{
    public partial class _InitialHts : Migration
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
                name: "HtsClients",
                columns: table => new
                {
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsRecencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientDisabled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClients", x => new { x.PatientPk, x.SiteCode });
                });

            migrationBuilder.CreateTable(
                name: "Manifests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sent = table.Column<int>(type: "int", nullable: false),
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
                    ManifestStatus = table.Column<int>(type: "int", nullable: false),
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
                name: "StageClientLinkages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEnrolled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrolledFacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePrefferedToBeEnrolled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandedOverTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandedOverToCadre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportedCCCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportedStartARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageClientLinkages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsRecencyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientDisabled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageClients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageHtsClientTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EverTestedForHiv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthsSinceLastTest = table.Column<int>(type: "int", nullable: true),
                    ClientTestedAs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalTestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientGivenResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TbScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSelfTested = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoupleDiscordant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Setting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsRiskCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsRiskScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageHtsClientTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageHtsClientTracing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TracingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageHtsClientTracing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageHtsEligibilityExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncounterId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityPopulation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHealthWorker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipWithContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestedHIVBefore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhoPerformedTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOfHIV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartedOnART = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverHadSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexuallyActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewPartner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoupleDiscordant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MultiplePartners = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfPartners = table.Column<int>(type: "int", nullable: true),
                    AlcoholSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoneySex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondomBurst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnknownStatusPartner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnownStatusPartner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pregnant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastfeedingMother = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperiencedGBV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverOnPrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyOnPrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverOnPep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyOnPep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverHadSTI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyHasSTI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverHadTB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SharedNeedle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeedleStickInjuries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraditionalProcedures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildReasonsForIneligibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibleForTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonsForIneligibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificReasonForIneligibility = table.Column<int>(type: "int", nullable: true),
                    MothersStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTestedSelf = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResultOfHIVSelf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTestedProvider = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScreenedTB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cough = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fever = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightLoss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NightSweats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lethargy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredForTesting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssessmentOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeGBV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForcedSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactWithTBCase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisabilityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HTSStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HTSEntryPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVRiskCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonRefferredForTesting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsRiskScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonNotReffered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageHtsEligibilityExtract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageHtsPartnerNotificationServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerPersonID = table.Column<int>(type: "int", nullable: true),
                    DateElicited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerPatientPk = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationsipToIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenedForIpv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpvScreeningOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PnsApproach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PnsConsent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedToCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkDateLinkedToCare = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CccNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityLinkedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndexPatientPk = table.Column<int>(type: "int", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageHtsPartnerNotificationServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageHtsPartnerTracings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerPersonId = table.Column<int>(type: "int", nullable: true),
                    TraceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TraceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageHtsPartnerTracings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageHtsTestKits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitLotNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitExpiry1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitLotNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitExpiry2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageHtsTestKits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "HtsClientLinkages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEnrolled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrolledFacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePrefferedToBeEnrolled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandedOverTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandedOverToCadre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportedCCCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportedStartARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_HtsClientLinkages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsClientLinkages_HtsClients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "HtsClients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsClientTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EverTestedForHiv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthsSinceLastTest = table.Column<int>(type: "int", nullable: true),
                    ClientTestedAs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntryPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalTestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientGivenResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TbScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientSelfTested = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoupleDiscordant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Setting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsRiskCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HtsRiskScore = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsClientTests_HtsClients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "HtsClients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsClientTracing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TracingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientTracing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsClientTracing_HtsClients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "HtsClients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsEligibilityExtract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncounterId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeyPopulation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityPopulation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHealthWorker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationshipWithContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestedHIVBefore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhoPerformedTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultOfHIV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartedOnART = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverHadSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SexuallyActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewPartner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoupleDiscordant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MultiplePartners = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfPartners = table.Column<int>(type: "int", nullable: true),
                    AlcoholSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoneySex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CondomBurst = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnknownStatusPartner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnownStatusPartner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pregnant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastfeedingMother = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperiencedGBV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverOnPrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyOnPrep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverOnPep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyOnPep = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverHadSTI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyHasSTI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EverHadTB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SharedNeedle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeedleStickInjuries = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraditionalProcedures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChildReasonsForIneligibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EligibleForTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonsForIneligibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecificReasonForIneligibility = table.Column<int>(type: "int", nullable: true),
                    MothersStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTestedSelf = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResultOfHIVSelf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTestedProvider = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScreenedTB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cough = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fever = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightLoss = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NightSweats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lethargy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredForTesting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssessmentOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeGBV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForcedSex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactWithTBCase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisabilityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HTSStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HTSEntryPoint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVRiskCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonRefferredForTesting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonNotReffered = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    HtsRiskScore = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsEligibilityExtract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsEligibilityExtract_HtsClients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "HtsClients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsPartnerNotificationServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerPersonID = table.Column<int>(type: "int", nullable: true),
                    DateElicited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerPatientPk = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationsipToIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreenedForIpv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpvScreeningOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentlyLivingWithIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnowledgeOfHivStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PnsApproach = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PnsConsent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkedToCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkDateLinkedToCare = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CccNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityLinkedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true),
                    IndexPatientPk = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsPartnerNotificationServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsPartnerNotificationServices_HtsClients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "HtsClients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsPartnerTracings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerPersonId = table.Column<int>(type: "int", nullable: true),
                    TraceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TraceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraceOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsPartnerTracings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsPartnerTracings_HtsClients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "HtsClients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsTestKits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitLotNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitExpiry1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitLotNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestKitExpiry2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsTestKits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsTestKits_HtsClients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "HtsClients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
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

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_ManifestId",
                table: "Cargoes",
                column: "ManifestId");

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientLinkages_PatientPk_SiteCode",
                table: "HtsClientLinkages",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientTests_PatientPk_SiteCode",
                table: "HtsClientTests",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientTracing_PatientPk_SiteCode",
                table: "HtsClientTracing",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsEligibilityExtract_PatientPk_SiteCode",
                table: "HtsEligibilityExtract",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsPartnerNotificationServices_PatientPk_SiteCode",
                table: "HtsPartnerNotificationServices",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsPartnerTracings_PatientPk_SiteCode",
                table: "HtsPartnerTracings",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsTestKits_PatientPk_SiteCode",
                table: "HtsTestKits",
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
                name: "HtsClientLinkages");

            migrationBuilder.DropTable(
                name: "HtsClientTests");

            migrationBuilder.DropTable(
                name: "HtsClientTracing");

            migrationBuilder.DropTable(
                name: "HtsEligibilityExtract");

            migrationBuilder.DropTable(
                name: "HtsPartnerNotificationServices");

            migrationBuilder.DropTable(
                name: "HtsPartnerTracings");

            migrationBuilder.DropTable(
                name: "HtsTestKits");

            migrationBuilder.DropTable(
                name: "MasterFacilities");

            migrationBuilder.DropTable(
                name: "StageClientLinkages");

            migrationBuilder.DropTable(
                name: "StageClients");

            migrationBuilder.DropTable(
                name: "StageHtsClientTests");

            migrationBuilder.DropTable(
                name: "StageHtsClientTracing");

            migrationBuilder.DropTable(
                name: "StageHtsEligibilityExtract");

            migrationBuilder.DropTable(
                name: "StageHtsPartnerNotificationServices");

            migrationBuilder.DropTable(
                name: "StageHtsPartnerTracings");

            migrationBuilder.DropTable(
                name: "StageHtsTestKits");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Manifests");

            migrationBuilder.DropTable(
                name: "HtsClients");

            migrationBuilder.DropTable(
                name: "Dockets");
        }
    }
}
