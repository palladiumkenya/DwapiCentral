using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwapiCentral.Mnch.Infrastructure.Migrations
{
    public partial class _initialMnchMigration : Migration
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
                name: "MnchPatients",
                columns: table => new
                {
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientHeiID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstEnrollmentAtMnch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentSubCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentWard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MnchPatients", x => new { x.PatientPk, x.SiteCode });
                });

            migrationBuilder.CreateTable(
                name: "StageAncVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ANCClinicNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANCVisitNo = table.Column<int>(type: "int", nullable: true),
                    GestationWeeks = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MUAC = table.Column<int>(type: "int", nullable: true),
                    BP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastExam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AntenatalExercises = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FGM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FGMComplications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Haemoglobin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiabetesTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CACxScreen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CACxScreenMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WHOStaging = table.Column<int>(type: "int", nullable: true),
                    VLSampleTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VLDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VLResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTreatment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVStatusBeforeANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestingDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestFinalResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTestDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTestResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherProphylaxisGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherGivenHAART = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AZTBabyDispense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NVPBabyDispense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChronicIllness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounselledOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVTestingANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVStatusANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostParturmFP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deworming = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MalariaProphylaxis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TetanusDose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IronSupplementsGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedMosquitoNet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventiveServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrinalysisVariables = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralReasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextAppointmentANC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HepatitisBScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatedHepatitisB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresumptiveTreatmentGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresumptiveTreatmentDose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiminumPackageOfCareReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiminumPackageOfCareServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageAncVisits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageCwcEnrolments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientIDCWC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEIID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MothersPkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationAtCWC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationAtHEI = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    Gestation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BirthOrder = table.Column<int>(type: "int", nullable: true),
                    BirthType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialNeeds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherAlive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MothersCCCNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferInDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEIDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NVP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastFeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ARTMother = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ARTRegimenMother = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ARTStartDateMother = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageCwcEnrolments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageCwcVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MUAC = table.Column<int>(type: "int", nullable: true),
                    WeightCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stunted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfantFeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicationGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MNPsSupplementation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Immunization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DangerSigns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Milestones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VitaminA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedMosquitoNet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dewormed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralReasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FollowUP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextAppointment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevisitThisYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferred = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeightLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ZScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZScoreAbsolute = table.Column<int>(type: "int", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageCwcVisits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageHeis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNAPCR1Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DNAPCR2Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DNAPCR3Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmatoryPCRDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BasellineVLDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalyAntibodyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DNAPCR1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DNAPCR2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DNAPCR3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmatoryPCR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasellineVL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalyAntibody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEIExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HEIHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEIExitCritearia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientHeiId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageHeis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageMatVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdmissionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANCVisits = table.Column<int>(type: "int", nullable: true),
                    DateOfDelivery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationOfDelivery = table.Column<int>(type: "int", nullable: true),
                    GestationAtBirth = table.Column<int>(type: "int", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlacentaComplete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UterotonicGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaginalExamination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodLoss = table.Column<int>(type: "int", nullable: true),
                    BloodLossVisual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditonAfterDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaternalDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryComplications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoBabiesDelivered = table.Column<int>(type: "int", nullable: true),
                    BabyBirthNumber = table.Column<int>(type: "int", nullable: true),
                    SexBaby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthWithDeformity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TetracyclineGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitiatedBF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApgarScore1 = table.Column<int>(type: "int", nullable: true),
                    ApgarScore5 = table.Column<int>(type: "int", nullable: true),
                    ApgarScore10 = table.Column<int>(type: "int", nullable: true),
                    KangarooCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChlorhexidineApplied = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VitaminKGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusBabyDischarge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherDischargeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTestResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVStatusLastANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestingDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIV1Results = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIV2Results = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestFinalResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnARTANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BabyGivenProphylaxis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherGivenCTX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVTestingMAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVStatusMAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounselledOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EDD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaternalDeathAudited = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnARTMat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageMatVisits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageMnchArts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientHeiID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartRegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusAtCCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityReceivingARTCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageMnchArts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageMnchEnrolments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollmentDateAtMnch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MnchNumber = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstVisitAnc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Parity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gravidae = table.Column<int>(type: "int", nullable: true),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EDDFromLMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HIVStatusBeforeANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartnerHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVTestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusAtMnch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageMnchEnrolments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageMnchImmunizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BCG = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OPVatBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OPV1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OPV2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OPV3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IPV = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DPTHepBHIB1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DPTHepBHIB2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DPTHepBHIB3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PCV101 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PCV102 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PCV103 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ROTA1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeaslesReubella1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YellowFever = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeaslesReubella2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeaslesAt6Months = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ROTA2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfNextVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BCGScarChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateChecked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateBCGrepeated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt6Months = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt1Yr = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt18Months = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt2Years = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt2To5Years = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FullyImmunizedChild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageMnchImmunizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageMnchLabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMNCH_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SatelliteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    OrderedbyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReportedbyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageMnchLabs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StageMnchPatients",
                columns: table => new
                {
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientHeiID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstEnrollmentAtMnch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentSubCounty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientResidentWard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InSchool = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NUPI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageMnchPatients", x => new { x.PatientPk, x.SiteCode });
                });

            migrationBuilder.CreateTable(
                name: "StageMotherBabyPairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BabyPatientPK = table.Column<int>(type: "int", nullable: false),
                    MotherPatientPK = table.Column<int>(type: "int", nullable: false),
                    BabyPatientMncHeiID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherPatientMncHeiID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientIDCCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StageMotherBabyPairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StagePncVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PNCRegisterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PNCVisitNo = table.Column<int>(type: "int", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MUAC = table.Column<int>(type: "int", nullable: true),
                    BP = table.Column<int>(type: "int", nullable: true),
                    BreastExam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasPallor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pallor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Breast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PPH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CSScar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UterusInvolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Episiotomy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lochia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fistula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaternalComplications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientScreenedCACx = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CACxScreenMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CACxScreenResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestingDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestFinalResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfantProphylaxisGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherProphylaxisGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoupleCounselled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVTestingPNC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVResultPNC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounselledOnFP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedFP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaematinicsGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BabyConditon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BabyFeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UmbilicalCord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Immunization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfantFeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventiveServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextAppointmentPNC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitTimingMother = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitTimingBaby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherCameForHIVTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfactCameForHAART = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherGivenHAART = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LiveStage = table.Column<int>(type: "int", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_StagePncVisits", x => x.Id);
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false)
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
                name: "AncVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ANCClinicNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANCVisitNo = table.Column<int>(type: "int", nullable: true),
                    GestationWeeks = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MUAC = table.Column<int>(type: "int", nullable: true),
                    BP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastExam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AntenatalExercises = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FGM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FGMComplications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Haemoglobin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiabetesTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CACxScreen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CACxScreenMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WHOStaging = table.Column<int>(type: "int", nullable: true),
                    VLSampleTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VLDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VLResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTreatment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVStatusBeforeANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestingDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestFinalResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTestDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTestType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTestResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTreated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherProphylaxisGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherGivenHAART = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AZTBabyDispense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NVPBabyDispense = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChronicIllness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounselledOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVTestingANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVStatusANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostParturmFP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deworming = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MalariaProphylaxis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TetanusDose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IronSupplementsGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedMosquitoNet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventiveServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrinalysisVariables = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralReasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextAppointmentANC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HepatitisBScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TreatedHepatitisB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresumptiveTreatmentGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PresumptiveTreatmentDose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiminumPackageOfCareReceived = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiminumPackageOfCareServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_AncVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AncVisits_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CwcEnrolments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientIDCWC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEIID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MothersPkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationAtCWC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationAtHEI = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    Gestation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BirthOrder = table.Column<int>(type: "int", nullable: true),
                    BirthType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialNeeds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherAlive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MothersCCCNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferIn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferInDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEIDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NVP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastFeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ARTMother = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ARTRegimenMother = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ARTStartDateMother = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_CwcEnrolments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CwcEnrolments_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CwcVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MUAC = table.Column<int>(type: "int", nullable: true),
                    WeightCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stunted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfantFeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicationGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBAssessment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MNPsSupplementation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Immunization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DangerSigns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Milestones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VitaminA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedMosquitoNet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dewormed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralReasons = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FollowUP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextAppointment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevisitThisYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refferred = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeightLength = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ZScore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZScoreAbsolute = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_CwcVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CwcVisits_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Heis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNAPCR1Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DNAPCR2Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DNAPCR3Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmatoryPCRDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BasellineVLDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalyAntibodyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DNAPCR1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DNAPCR2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DNAPCR3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmatoryPCR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasellineVL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalyAntibody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEIExitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HEIHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEIExitCritearia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientHeiId = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Heis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Heis_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdmissionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANCVisits = table.Column<int>(type: "int", nullable: true),
                    DateOfDelivery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationOfDelivery = table.Column<int>(type: "int", nullable: true),
                    GestationAtBirth = table.Column<int>(type: "int", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlacentaComplete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UterotonicGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VaginalExamination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodLoss = table.Column<int>(type: "int", nullable: true),
                    BloodLossVisual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditonAfterDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaternalDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryComplications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoBabiesDelivered = table.Column<int>(type: "int", nullable: true),
                    BabyBirthNumber = table.Column<int>(type: "int", nullable: true),
                    SexBaby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthWithDeformity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TetracyclineGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitiatedBF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApgarScore1 = table.Column<int>(type: "int", nullable: true),
                    ApgarScore5 = table.Column<int>(type: "int", nullable: true),
                    ApgarScore10 = table.Column<int>(type: "int", nullable: true),
                    KangarooCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChlorhexidineApplied = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VitaminKGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusBabyDischarge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherDischargeDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SyphilisTestResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVStatusLastANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestingDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIV1Results = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIV2Results = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestFinalResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnARTANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BabyGivenProphylaxis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherGivenCTX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVTestingMAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVStatusMAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounselledOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EDD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaternalDeathAudited = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferralReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnARTMat = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MatVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatVisits_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MnchArts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pkv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientHeiID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationAtCCC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartRegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusAtCCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastARTDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastRegimen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastRegimenLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityReceivingARTCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MnchArts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MnchArts_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MnchEnrolments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnrollmentDateAtMnch = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MnchNumber = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstVisitAnc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Parity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gravidae = table.Column<int>(type: "int", nullable: true),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EDDFromLMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HIVStatusBeforeANC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PartnerHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVTestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusAtMnch = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MnchEnrolments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MnchEnrolments_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MnchImmunizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BCG = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OPVatBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OPV1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OPV2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OPV3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IPV = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DPTHepBHIB1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DPTHepBHIB2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DPTHepBHIB3 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PCV101 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PCV102 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PCV103 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ROTA1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeaslesReubella1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YellowFever = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeaslesReubella2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MeaslesAt6Months = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ROTA2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfNextVisit = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BCGScarChecked = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateChecked = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateBCGrepeated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt6Months = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt1Yr = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt18Months = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt2Years = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VitaminAAt2To5Years = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FullyImmunizedChild = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MnchImmunizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MnchImmunizations_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MnchLabs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMNCH_ID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SatelliteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    OrderedbyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReportedbyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TestName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LabReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MnchLabs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MnchLabs_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotherBabyPairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BabyPatientPK = table.Column<int>(type: "int", nullable: false),
                    MotherPatientPK = table.Column<int>(type: "int", nullable: false),
                    BabyPatientMncHeiID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherPatientMncHeiID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientIDCCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_MotherBabyPairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotherBabyPairs_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PncVisits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    RecordUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMnchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitID = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PNCRegisterNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PNCVisitNo = table.Column<int>(type: "int", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModeOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfDelivery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MUAC = table.Column<int>(type: "int", nullable: true),
                    BP = table.Column<int>(type: "int", nullable: true),
                    BreastExam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeneralCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasPallor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pallor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Breast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PPH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CSScar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UterusInvolution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Episiotomy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lochia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fistula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaternalComplications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBScreening = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientScreenedCACx = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CACxScreenMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CACxScreenResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorHIVStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestingDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest1Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTest2Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HIVTestFinalResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfantProphylaxisGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherProphylaxisGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoupleCounselled = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVTestingPNC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartnerHIVResultPNC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounselledOnFP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedFP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaematinicsGiven = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryOutcome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BabyConditon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BabyFeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UmbilicalCord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Immunization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfantFeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreventiveServices = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextAppointmentPNC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitTimingMother = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VisitTimingBaby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherCameForHIVTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfactCameForHAART = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherGivenHAART = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_PncVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PncVisits_MnchPatients_PatientPk_SiteCode",
                        columns: x => new { x.PatientPk, x.SiteCode },
                        principalTable: "MnchPatients",
                        principalColumns: new[] { "PatientPk", "SiteCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AncVisits_PatientPk_SiteCode",
                table: "AncVisits",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_ManifestId",
                table: "Cargoes",
                column: "ManifestId");

            migrationBuilder.CreateIndex(
                name: "IX_CwcEnrolments_PatientPk_SiteCode",
                table: "CwcEnrolments",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_CwcVisits_PatientPk_SiteCode",
                table: "CwcVisits",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Heis_PatientPk_SiteCode",
                table: "Heis",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_MatVisits_PatientPk_SiteCode",
                table: "MatVisits",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_MnchArts_PatientPk_SiteCode",
                table: "MnchArts",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_MnchEnrolments_PatientPk_SiteCode",
                table: "MnchEnrolments",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_MnchImmunizations_PatientPk_SiteCode",
                table: "MnchImmunizations",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_MnchLabs_PatientPk_SiteCode",
                table: "MnchLabs",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_MotherBabyPairs_PatientPk_SiteCode",
                table: "MotherBabyPairs",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_PncVisits_PatientPk_SiteCode",
                table: "PncVisits",
                columns: new[] { "PatientPk", "SiteCode" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_DocketId",
                table: "Subscribers",
                column: "DocketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AncVisits");

            migrationBuilder.DropTable(
                name: "Cargoes");

            migrationBuilder.DropTable(
                name: "CwcEnrolments");

            migrationBuilder.DropTable(
                name: "CwcVisits");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Heis");

            migrationBuilder.DropTable(
                name: "MasterFacilities");

            migrationBuilder.DropTable(
                name: "MatVisits");

            migrationBuilder.DropTable(
                name: "MnchArts");

            migrationBuilder.DropTable(
                name: "MnchEnrolments");

            migrationBuilder.DropTable(
                name: "MnchImmunizations");

            migrationBuilder.DropTable(
                name: "MnchLabs");

            migrationBuilder.DropTable(
                name: "MotherBabyPairs");

            migrationBuilder.DropTable(
                name: "PncVisits");

            migrationBuilder.DropTable(
                name: "StageAncVisits");

            migrationBuilder.DropTable(
                name: "StageCwcEnrolments");

            migrationBuilder.DropTable(
                name: "StageCwcVisits");

            migrationBuilder.DropTable(
                name: "StageHeis");

            migrationBuilder.DropTable(
                name: "StageMatVisits");

            migrationBuilder.DropTable(
                name: "StageMnchArts");

            migrationBuilder.DropTable(
                name: "StageMnchEnrolments");

            migrationBuilder.DropTable(
                name: "StageMnchImmunizations");

            migrationBuilder.DropTable(
                name: "StageMnchLabs");

            migrationBuilder.DropTable(
                name: "StageMnchPatients");

            migrationBuilder.DropTable(
                name: "StageMotherBabyPairs");

            migrationBuilder.DropTable(
                name: "StagePncVisits");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Manifests");

            migrationBuilder.DropTable(
                name: "MnchPatients");

            migrationBuilder.DropTable(
                name: "Dockets");
        }
    }
}
