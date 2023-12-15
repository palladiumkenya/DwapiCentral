namespace DwapiCentral.Mnch.Domain.Exceptions;

public class ManifestAlreadyExistsException : Exception
{
    public ManifestAlreadyExistsException(Guid id)
        : base($"Manifest with ID \"{id}\" already exists.")
    {
    }
}