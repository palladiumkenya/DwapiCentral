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
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA2-C680C13AC8E8"),  PatientPk = 3, SiteCode = -10000 },
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA3-C680C13AC8E8"), PatientPk = 4, SiteCode = -10000 }
        };
    }
    
    public static List<PatientVisitExtract> GetTestPatientVisitExtractsUpdates()
    {
        return new List<PatientVisitExtract>()
        {
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA2-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000},
            new PatientVisitExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA3-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000}
        };
    }

    
}
