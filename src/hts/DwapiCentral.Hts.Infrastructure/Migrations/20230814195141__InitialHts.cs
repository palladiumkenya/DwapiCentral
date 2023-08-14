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
                name: "Clients",
                columns: table => new
                {
                    HtsNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyPop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestedBefore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthsLastTested = table.Column<int>(type: "int", nullable: true),
                    ClientTestedAs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrategyHTS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitLotNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitExpiryDate1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestResultsHTS1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitLotNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitExpiryDate2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResultsHTS2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalResultHTS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalResultsGiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TBScreeningHTS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientSelfTested = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoupleDiscordant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientDisabled = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisabilityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientConsented = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCounty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUPI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => new { x.PatientPk, x.SiteCode, x.HtsNumber });
                });

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
                    Sent = table.Column<int>(type: "int", nullable: false),
                    Recieved = table.Column<int>(type: "int", nullable: false),
                    DateLogged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateArrived = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                name: "StageClientLinkages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneTracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhysicalTracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CccNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrolledFacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnrolled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePrefferedToBeEnrolled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HandedOverTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HandedOverToCadre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportedCCCNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportedStartARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageClientLinkages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageClientPartners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageClientPartners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyPop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestedBefore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthsLastTested = table.Column<int>(type: "int", nullable: true),
                    ClientTestedAs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrategyHTS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitLotNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitExpiryDate1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestResultsHTS1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitLotNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitExpiryDate2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResultsHTS2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalResultHTS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalResultsGiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TBScreeningHTS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientSelfTested = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoupleDiscordant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientDisabled = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisabilityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientConsented = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCounty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NUPI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EverTestedForHiv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthsSinceLastTest = table.Column<int>(type: "int", nullable: true),
                    ClientTestedAs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestStrategy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResult1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResult2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalTestResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientGivenResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TbScreening = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientSelfTested = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoupleDiscordant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Consent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageHtsClientTracing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageHtsPartnerNotificationServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartnerPatientPk = table.Column<int>(type: "int", nullable: true),
                    PartnerPersonID = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelationsipToIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenedForIpv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpvScreeningOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentlyLivingWithIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KnowledgeOfHivStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PnsApproach = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PnsConsent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedToCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkDateLinkedToCare = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CccNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityLinkedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateElicited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TraceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TraceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TraceOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartnerPersonID = table.Column<int>(type: "int", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    TestKitName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitLotNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitExpiry1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResult1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitLotNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitExpiry2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResult2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StageHtsTestKits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientLinkages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneTracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhysicalTracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CccNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrolledFacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferralDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateEnrolled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePrefferedToBeEnrolled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HandedOverTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HandedOverToCadre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportedCCCNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportedStartARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientLinkages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientLinkages_Clients_PatientPk_SiteCode_HtsNumber",
                        columns: x => new { x.PatientPk, x.SiteCode, x.HtsNumber },
                        principalTable: "Clients",
                        principalColumns: new[] { "PatientPk", "SiteCode", "HtsNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientPartners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPartners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPartners_Clients_PatientPk_SiteCode_HtsNumber",
                        columns: x => new { x.PatientPk, x.SiteCode, x.HtsNumber },
                        principalTable: "Clients",
                        principalColumns: new[] { "PatientPk", "SiteCode", "HtsNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsClientTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    TestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EverTestedForHiv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthsSinceLastTest = table.Column<int>(type: "int", nullable: true),
                    ClientTestedAs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntryPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestStrategy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResult1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResult2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalTestResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientGivenResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TbScreening = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientSelfTested = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoupleDiscordant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Consent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsClientTests_Clients_PatientPk_SiteCode_HtsNumber",
                        columns: x => new { x.PatientPk, x.SiteCode, x.HtsNumber },
                        principalTable: "Clients",
                        principalColumns: new[] { "PatientPk", "SiteCode", "HtsNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsClientTracing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TracingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TracingOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsClientTracing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsClientTracing_Clients_PatientPk_SiteCode_HtsNumber",
                        columns: x => new { x.PatientPk, x.SiteCode, x.HtsNumber },
                        principalTable: "Clients",
                        principalColumns: new[] { "PatientPk", "SiteCode", "HtsNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsPartnerNotificationServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartnerPatientPk = table.Column<int>(type: "int", nullable: true),
                    PartnerPersonID = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelationsipToIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenedForIpv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpvScreeningOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentlyLivingWithIndexClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KnowledgeOfHivStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PnsApproach = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PnsConsent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedToCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkDateLinkedToCare = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CccNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityLinkedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateElicited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsPartnerNotificationServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsPartnerNotificationServices_Clients_PatientPk_SiteCode_HtsNumber",
                        columns: x => new { x.PatientPk, x.SiteCode, x.HtsNumber },
                        principalTable: "Clients",
                        principalColumns: new[] { "PatientPk", "SiteCode", "HtsNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsPartnerTracings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TraceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TraceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TraceOutcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PartnerPersonID = table.Column<int>(type: "int", nullable: true),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsPartnerTracings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsPartnerTracings_Clients_PatientPk_SiteCode_HtsNumber",
                        columns: x => new { x.PatientPk, x.SiteCode, x.HtsNumber },
                        principalTable: "Clients",
                        principalColumns: new[] { "PatientPk", "SiteCode", "HtsNumber" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HtsTestKits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HtsNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Processed = table.Column<bool>(type: "bit", nullable: true),
                    QueueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateExtracted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EncounterId = table.Column<int>(type: "int", nullable: true),
                    TestKitName1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitLotNumber1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitExpiry1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResult1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitLotNumber2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestKitExpiry2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestResult2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Emr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date_Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Extracted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtsTestKits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HtsTestKits_Clients_PatientPk_SiteCode_HtsNumber",
                        columns: x => new { x.PatientPk, x.SiteCode, x.HtsNumber },
                        principalTable: "Clients",
                        principalColumns: new[] { "PatientPk", "SiteCode", "HtsNumber" },
                        onDelete: ReferentialAction.Cascade);
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
                name: "Cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "IX_ClientLinkages_PatientPk_SiteCode_HtsNumber",
                table: "ClientLinkages",
                columns: new[] { "PatientPk", "SiteCode", "HtsNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientPartners_PatientPk_SiteCode_HtsNumber",
                table: "ClientPartners",
                columns: new[] { "PatientPk", "SiteCode", "HtsNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientTests_PatientPk_SiteCode_HtsNumber",
                table: "HtsClientTests",
                columns: new[] { "PatientPk", "SiteCode", "HtsNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsClientTracing_PatientPk_SiteCode_HtsNumber",
                table: "HtsClientTracing",
                columns: new[] { "PatientPk", "SiteCode", "HtsNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsPartnerNotificationServices_PatientPk_SiteCode_HtsNumber",
                table: "HtsPartnerNotificationServices",
                columns: new[] { "PatientPk", "SiteCode", "HtsNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsPartnerTracings_PatientPk_SiteCode_HtsNumber",
                table: "HtsPartnerTracings",
                columns: new[] { "PatientPk", "SiteCode", "HtsNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_HtsTestKits_PatientPk_SiteCode_HtsNumber",
                table: "HtsTestKits",
                columns: new[] { "PatientPk", "SiteCode", "HtsNumber" });

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
                name: "ClientLinkages");

            migrationBuilder.DropTable(
                name: "ClientPartners");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "HtsClientTests");

            migrationBuilder.DropTable(
                name: "HtsClientTracing");

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
                name: "StageClientPartners");

            migrationBuilder.DropTable(
                name: "StageClients");

            migrationBuilder.DropTable(
                name: "StageHtsClientTests");

            migrationBuilder.DropTable(
                name: "StageHtsClientTracing");

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
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Dockets");
        }
    }
}
