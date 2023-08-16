namespace DwapiCentral.Contracts.Common;

public interface IExtract
{
    int PatientPk { get; set; }
    int SiteCode { get; set; }
    string RecordUUID { get; set; }
    DateTime? Date_Created { get; set; }
    DateTime? DateLastModified { get; set; }
    DateTime? DateExtracted { get; set; }
    DateTime? Created { get; set; } 
    DateTime? Updated { get; set; }
    bool? Voided { get; set; }
}
