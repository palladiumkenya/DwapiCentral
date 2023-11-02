using DwapiCentral.Ct.Domain.Models;

namespace DwapiCentral.Ct.Domain.Repository;

public interface IArtFastTrackRepository
{
    Task<ArtFastTrackExtract> GetExtractByUniqueIdentifiers(int patientPK, int siteCode, string recordUUID);
    Task UpdateExtract(List<ArtFastTrackExtract> patientArtFastTrackExtract);
    Task InsertExtract(List<ArtFastTrackExtract> patientArtFastTrackExtract);

}