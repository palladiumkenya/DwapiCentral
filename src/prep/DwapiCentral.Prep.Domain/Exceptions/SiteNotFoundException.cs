namespace DwapiCentral.Prep.Domain.Exceptions;

public class SiteNotFoundInMflException : Exception
{
    public SiteNotFoundInMflException(int code)
        : base($"Facility with MFLCode \"{code}\" is not found in DWH Master Facility List.")
    {
    }
}