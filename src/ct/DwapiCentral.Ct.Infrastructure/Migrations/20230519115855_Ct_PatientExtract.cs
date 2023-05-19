using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DwapiCentral.Ct.Infrastructure.Migrations
{
    public partial class Ct_PatientExtract : Migration
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
                    Docket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Project = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DwapiVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmrSetup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmrId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmrName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmrVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    CccNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "PatientVisitExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientPk = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<int>(type: "int", nullable: false),
                    VisitId = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WHOStage = table.Column<int>(type: "int", nullable: true),
                    WABStage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pregnant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LMP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EDD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OIDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubstitutionFirstlineRegimenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubstitutionFirstlineRegimenReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubstitutionSecondlineRegimenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubstitutionSecondlineRegimenReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondlineRegimenChangeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SecondlineRegimenChangeReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adherence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdherenceCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyPlanningMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PwP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GestationAge = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StabilityAssessment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DifferentiatedCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PopulationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyPopulationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temp = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PulseRate = table.Column<int>(type: "int", nullable: true),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: true),
                    OxygenSaturation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Muac = table.Column<int>(type: "int", nullable: true),
                    NutritionalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EverHadMenses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Breastfeeding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Menopausal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoFPReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProphylaxisUsed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CTXAdherence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentRegimen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HCWConcern = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TCAReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicalNotes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralExamination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemExamination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eyes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ENT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Chest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abdomen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genitourinary = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManifestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Metrics_ManifestId",
                table: "Metrics",
                column: "ManifestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "MasterFacilities");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropTable(
                name: "PatientExtracts");

            migrationBuilder.DropTable(
                name: "PatientVisitExtracts");

            migrationBuilder.DropTable(
                name: "Manifests");
        }
    }
}
