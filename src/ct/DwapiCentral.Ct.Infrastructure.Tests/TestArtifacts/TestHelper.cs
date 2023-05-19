using DwapiCentral.Ct.Domain.Models.Extracts;

namespace DwapiCentral.Ct.Infrastructure.Tests.TestArtifacts;

public class TestHelper
{
    public static List<PatientExtract> GetTestPatientExtractsNew()
    {
        return new List<PatientExtract>()
        {
            new PatientExtract() { PatientPk = 3, SiteCode = -10000, CccNumber = "C03" },
            new PatientExtract() { PatientPk = 4, SiteCode = -10000, CccNumber = "C04" }
        };
    }
    
    public static List<PatientExtract> GetTestPatientExtractsUpdates()
    {
        return new List<PatientExtract>()
        {
            new PatientExtract() { PatientPk = 1, SiteCode = -10000, CccNumber = "xC01",Nupi = "N01"},
            new PatientExtract() { PatientPk = 2, SiteCode = -10000, CccNumber = "xC02",Nupi = "N01" }
        };
    }
    
    
    public static List<PatientVisitExtract> GetTestPatientVisitExtractsNew()
    {
        return new List<PatientVisitExtract>()
        {
            new PatientVisitExtract() { PatientPk = 3, SiteCode = -10000 },
            new PatientVisitExtract() { PatientPk = 4, SiteCode = -10000 }
        };
    }
    
    public static List<PatientVisitExtract> GetTestPatientVisitExtractsUpdates()
    {
        return new List<PatientVisitExtract>()
        {
            new PatientVisitExtract() { PatientPk = 1, SiteCode = -10000},
            new PatientVisitExtract() { PatientPk = 2, SiteCode = -10000}
        };
    }

    
}