﻿// <auto-generated />
using System;
using DwapiCentral.Ct.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DwapiCentral.Ct.Infrastructure.Migrations
{
    [DbContext(typeof(CtDbContext))]
    partial class CtDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.Extracts.PatientExtract", b =>
                {
                    b.Property<int>("PatientPk")
                        .HasColumnType("int");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.Property<string>("CccNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactRelation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateConfirmedHIVPositive")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateExtracted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateLastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("District")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EducationLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Inschool")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeyPopulationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastVisit")
                        .HasColumnType("datetime2");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MpiId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nupi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Orphan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientResidentCounty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientResidentLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientResidentSubCounty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientResidentSubLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientResidentVillage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientResidentWard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pkv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PopulationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviousARTExposure")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PreviousARTStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Region")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RegistrationATPMTCT")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RegistrationAtCCC")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RegistrationAtTBClinic")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StatusAtCCC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusAtPMTCT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusAtTBClinic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TransferInDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Village")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Voided")
                        .HasColumnType("bit");

                    b.HasKey("PatientPk", "SiteCode");

                    b.ToTable("PatientExtracts");
                });

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.Extracts.PatientVisitExtract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Abdomen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adherence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdherenceCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Breastfeeding")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CNS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CTXAdherence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CVS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chest")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClinicalNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentRegimen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateExtracted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateLastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("DifferentiatedCare")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EDD")
                        .HasColumnType("datetime2");

                    b.Property<string>("ENT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EverHadMenses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Eyes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyPlanningMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GeneralExamination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genitourinary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("GestationAge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("HCWConcern")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("KeyPopulationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LMP")
                        .HasColumnType("datetime2");

                    b.Property<string>("Menopausal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Muac")
                        .HasColumnType("int");

                    b.Property<DateTime?>("NextAppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoFPReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NutritionalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OIDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("OxygenSaturation")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PatientPk")
                        .HasColumnType("int");

                    b.Property<string>("PopulationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pregnant")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProphylaxisUsed")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PulseRate")
                        .HasColumnType("int");

                    b.Property<string>("PwP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefillDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RespiratoryRate")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SecondlineRegimenChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecondlineRegimenChangeReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.Property<string>("Skin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StabilityAssessment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SubstitutionFirstlineRegimenDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubstitutionFirstlineRegimenReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SubstitutionSecondlineRegimenDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SubstitutionSecondlineRegimenReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemExamination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TCAReason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Temp")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("VisitBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("VisitDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("VisitId")
                        .HasColumnType("int");

                    b.Property<string>("VisitType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Voided")
                        .HasColumnType("bit");

                    b.Property<string>("WABStage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WHOStage")
                        .HasColumnType("int");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PatientPk", "SiteCode");

                    b.ToTable("PatientVisitExtracts");
                });

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.Facility", b =>
                {
                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.Manifest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Docket")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DwapiVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmrId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmrName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmrSetup")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmrVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Project")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Session")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SiteCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StatusDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UploadMode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Manifests");
                });

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.MasterFacility", b =>
                {
                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("MasterFacilities");
                });

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.Metric", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ManifestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ManifestId");

                    b.ToTable("Metrics");
                });

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.Extracts.PatientVisitExtract", b =>
                {
                    b.HasOne("DwapiCentral.Ct.Domain.Models.Extracts.PatientExtract", "Patients")
                        .WithMany("PatientVisitExtracts")
                        .HasForeignKey("PatientPk", "SiteCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.Metric", b =>
                {
                    b.HasOne("DwapiCentral.Ct.Domain.Models.Manifest", null)
                        .WithMany("Metrics")
                        .HasForeignKey("ManifestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.Extracts.PatientExtract", b =>
                {
                    b.Navigation("PatientVisitExtracts");
                });

            modelBuilder.Entity("DwapiCentral.Ct.Domain.Models.Manifest", b =>
                {
                    b.Navigation("Metrics");
                });
#pragma warning restore 612, 618
        }
    }
}
