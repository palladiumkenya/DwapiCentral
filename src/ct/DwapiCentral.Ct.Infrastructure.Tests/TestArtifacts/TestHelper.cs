using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Infrastructure.Tests.TestArtifacts;

public class TestHelper
{
    public static List<PatientExtract> GetTestPatientExtractsNew()
    {
        return new List<PatientExtract>()
        {
            new PatientExtract() { PatientPk = 3, SiteCode = -10000, CccNumber = "C03", Gender="F" },
            new PatientExtract() { PatientPk = 4, SiteCode = -10000, CccNumber = "C04", Gender = "M" }
        };
    }
    
    public static List<PatientExtract> GetTestPatientExtractsUpdates()
    {
        return new List<PatientExtract>()
        {
            new PatientExtract() { PatientPk = 1, SiteCode = -10000,Gender="F", CccNumber = "xC01",Nupi = "N01"},
            new PatientExtract() { PatientPk = 2, SiteCode = -10000,Gender="M" ,CccNumber = "xC02",Nupi = "N02" }
        };
    }
    
    
    public static List<PatientVisitExtract> GetTestPatientVisitExtractsNew()
    {
        return new List<PatientVisitExtract>()
        {
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA3-C680C13AC8E8"),  PatientPk = 3, SiteCode = -10000 ,VisitId=003,VisitDate=DateTime.Today.AddDays(-5)},
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA4-C680C13AC8E8"),  PatientPk = 4, SiteCode = -10000 ,VisitId = 004,VisitDate=DateTime.Today.AddDays(-6)}
        };
    }
    
    public static List<PatientVisitExtract> GetTestPatientVisitExtractsUpdates()
    {
        return new List<PatientVisitExtract>()
        {
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitId=001,VisitDate=DateTime.Now,Weight=67},
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitId=005,VisitDate=DateTime.Now,Height=162},
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitId=006,VisitDate=DateTime.Today.AddDays(-2),Height = 163,Weight=56},
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitId=007,VisitDate = DateTime.Today.AddDays(-1),Height=163,Weight=58}

        };
    }
    public static List<PatientPharmacyExtract> GetTestPatientPharmacyExtractsNew()
    {
        return new List<PatientPharmacyExtract>()
        {
            new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA3-C680C13AC8E8"),  PatientPk = 3, SiteCode = -10000 ,VisitID=003,DispenseDate=DateTime.Today.AddDays(-5)},
            new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA4-C680C13AC8E8"),  PatientPk = 4, SiteCode = -10000 ,VisitID = 004,DispenseDate=DateTime.Today.AddDays(-6)}
        };
    }

    public static List<PatientPharmacyExtract> GetTestPatientPharmacyExtractsUpdates()
    {
        return new List<PatientPharmacyExtract>()
        {
            new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=001,DispenseDate=DateTime.Now,Drug="prophylaxis"},
            new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=005,DispenseDate=DateTime.Now,Provider="xyz"},
            new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=006,DispenseDate=DateTime.Today.AddDays(-2),Drug = "prop",Provider="mj"},
            new PatientPharmacyExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=007,DispenseDate = DateTime.Today.AddDays(-1),Provider="curry",Drug="ashy"}

        };
    }


}
