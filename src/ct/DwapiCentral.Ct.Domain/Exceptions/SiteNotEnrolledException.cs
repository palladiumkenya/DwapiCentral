namespace DwapiCentral.Ct.Domain.Exceptions;

public class SiteNotEnrolledException : Exception
{
    public SiteNotEnrolledException(int code)
        : base($"Facility with MFLCode \"{code}\" is not ENROLLED in DWH.")
    {
    }
}