namespace DwapiCentral.Ct.Domain.Exceptions;

public class OutDatedDwapiException : Exception
{
    public OutDatedDwapiException(string version, string expectedVersion)
        : base($"You are using an outdated DWAPI version \"{version}\" please upgrade to version \"{expectedVersion}\" or earlier")
    {
    }
}