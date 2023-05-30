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

    public static List<PatientLaboratoryExtract> GetTestPatientLabExtractsUpdates()
    {
        return new List<PatientLaboratoryExtract>()
        {
            new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitId=1001,OrderedByDate=DateTime.Now,TestName="abc"},
            new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitId=1005,OrderedByDate=DateTime.Now,TestResult="xyz"},
            new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitId=1006,OrderedByDate=DateTime.Today.AddDays(-2),TestName = "prop",TestResult="mj"},
            new PatientLaboratoryExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitId=1007,OrderedByDate = DateTime.Today.AddDays(-1),TestResult="curry",TestName="ashy"}

        };
    }

    public static List<PatientArtExtract> GetTestPatientArtUpdates()
    {
        return new List<PatientArtExtract>()
        {
            new PatientArtExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,LastARTDate=DateTime.Now,PreviousARTPurpose="abc"},
            new PatientArtExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,LastARTDate=DateTime.Now,AgeEnrollment=54},
            new PatientArtExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,LastARTDate=DateTime.Today.AddDays(-2),PreviousARTPurpose = "prop",AgeEnrollment=34},
            new PatientArtExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,LastARTDate = DateTime.Today.AddDays(-1),AgeEnrollment=26,PreviousARTPurpose="ashy"}

        };
    }
    public static List<PatientStatusExtract> GetTestPatientStatusUpdates()
    {
        return new List<PatientStatusExtract>()
        {
            new PatientStatusExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,ExitDate=DateTime.Today.AddDays(-2)},
            new PatientStatusExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,ExitDate=DateTime.Today.AddDays(-3)},
            new PatientStatusExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,ExitDate=DateTime.Today.AddDays(-4)},
            new PatientStatusExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,ExitDate=DateTime.Today.AddDays(-5)}

        };
    }
    

    public static List<PatientBaselinesExtract> GetTestPatientBaselinesExtractsUpdates()
    {
        return new List<PatientBaselinesExtract>()
        {
            new PatientBaselinesExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,DateCreated=DateTime.Now},
            new PatientBaselinesExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,DateCreated=DateTime.Today.AddDays(-4)},
            new PatientBaselinesExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,DateCreated=DateTime.Today.AddDays(-3)},
            new PatientBaselinesExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000, DateCreated = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<PatientAdverseEventExtract> GetTestPatientAdverseExtractsUpdates()
    {
        return new List<PatientAdverseEventExtract>()
        {
            new PatientAdverseEventExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitDate=DateTime.Now},
            new PatientAdverseEventExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitDate=DateTime.Today.AddDays(-4)},
            new PatientAdverseEventExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitDate=DateTime.Today.AddDays(-3)},
            new PatientAdverseEventExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<OvcExtract> GetTestPatientOvcExtracts()
    {
        return new List<OvcExtract>()
        {
            new OvcExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Now},
            new OvcExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 102,VisitDate=DateTime.Today.AddDays(-4)},
            new OvcExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,VisitDate=DateTime.Today.AddDays(-3)},
            new OvcExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<OtzExtract> GetTestPatientOtzExtracts()
    {
        return new List<OtzExtract>()
        {
            new OtzExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Now},
            new OtzExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 102,VisitDate=DateTime.Today.AddDays(-4)},
            new OtzExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,VisitDate=DateTime.Today.AddDays(-3)},
            new OtzExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<IptExtract> GetTestPatientIptExtracts()
    {
        return new List<IptExtract>()
        {
            new () {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Now},
            new IptExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 102,VisitDate=DateTime.Today.AddDays(-4)},
            new IptExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,VisitDate=DateTime.Today.AddDays(-3)},
            new IptExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<GbvScreeningExtract> GetTestPatientGbvExtracts()
    {
        return new List<GbvScreeningExtract>()
        {
            new GbvScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Now},
            new GbvScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 102,VisitDate=DateTime.Today.AddDays(-4)},
            new GbvScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,VisitDate=DateTime.Today.AddDays(-3)},
            new GbvScreeningExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<EnhancedAdherenceCounsellingExtract> GetTestPatientEnhancedAdherance()
    {
        return new List<EnhancedAdherenceCounsellingExtract>()
        {
            new EnhancedAdherenceCounsellingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Now},
            new EnhancedAdherenceCounsellingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 102,VisitDate=DateTime.Today.AddDays(-4)},
            new EnhancedAdherenceCounsellingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,VisitDate=DateTime.Today.AddDays(-3)},
            new EnhancedAdherenceCounsellingExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<DrugAlcoholScreeningExtract> GetTestPatientDrugAlcoholScreening()
    {
        return new List<DrugAlcoholScreeningExtract>()
        {
            new DrugAlcoholScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Now},
            new DrugAlcoholScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 102,VisitDate=DateTime.Today.AddDays(-4)},
            new DrugAlcoholScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,VisitDate=DateTime.Today.AddDays(-3)},
            new DrugAlcoholScreeningExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }
    public static List<DepressionScreeningExtract> GetTestPatientDepressionScreening()
    {
        return new List<DepressionScreeningExtract>()
        {
            new DepressionScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Now},
            new DepressionScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 102,VisitDate=DateTime.Today.AddDays(-4)},
            new DepressionScreeningExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,VisitDate=DateTime.Today.AddDays(-3)},
            new DepressionScreeningExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<DefaulterTracingExtract> GetTestPatientDefaulterTracing()
    {
        return new List<DefaulterTracingExtract>()
        {
            new DefaulterTracingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Now},
            new DefaulterTracingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 102,VisitDate=DateTime.Today.AddDays(-4)},
            new DefaulterTracingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,VisitDate=DateTime.Today.AddDays(-3)},
            new DefaulterTracingExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<CovidExtract> GetTestPatientCovid()
    {
        return new List<CovidExtract>()
        {
            new CovidExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,Covid19AssessmentDate=DateTime.Now},
            new CovidExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 102,Covid19AssessmentDate=DateTime.Today.AddDays(-4)},
            new CovidExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,Covid19AssessmentDate=DateTime.Today.AddDays(-3)},
            new CovidExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, Covid19AssessmentDate = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<ContactListingExtract> GetTestPatientContactListing()
    {
        return new List<ContactListingExtract>()
        {
            new ContactListingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,DateCreated=DateTime.Now},
            new ContactListingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,DateCreated=DateTime.Today.AddDays(-4)},
            new ContactListingExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,DateCreated=DateTime.Today.AddDays(-3)},
            new ContactListingExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,DateCreated = DateTime.Today.AddDays(-2)}

        };
    }

    public static List<AllergiesChronicIllnessExtract> GetTestPatientAllergiesChronicIllness()
    {
        return new List<AllergiesChronicIllnessExtract>()
        {
            new AllergiesChronicIllnessExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA5-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID=101,VisitDate=DateTime.Today.AddDays(-1)},
            new AllergiesChronicIllnessExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA6-C680C13AC8E8"), PatientPk = 1, SiteCode = -10000,VisitID = 101,VisitDate=DateTime.Today.AddDays(-1)},
            new AllergiesChronicIllnessExtract() {Id=new Guid("017EC6FE-A65F-4F3E-AEA7-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID = 103,VisitDate=DateTime.Today.AddDays(-3)},
            new AllergiesChronicIllnessExtract() {Id = new Guid("017EC6FE-A65F-4F3E-AEA8-C680C13AC8E8"), PatientPk = 2, SiteCode = -10000,VisitID=104, VisitDate = DateTime.Today.AddDays(-2)}

        };
    }


}
