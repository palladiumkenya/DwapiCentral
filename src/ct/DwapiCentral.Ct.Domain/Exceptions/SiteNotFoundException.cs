namespace DwapiCentral.Ct.Domain.Exceptions;

public class SiteNotFoundException : Exception
{
    public SiteNotFoundException(int code)
        : base($"Facility with MFLCode \"{code}\" is not found in DWH Master Facility List.")
    {
    }
}