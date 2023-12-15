namespace DwapiCentral.Prep.Domain.Exceptions;

public class ManifestNotFoundException : Exception
{
    public ManifestNotFoundException(Guid id)
        : base($"Manifest with ID \"{id}\" not found.")
    {
    }
}