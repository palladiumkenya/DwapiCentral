using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using MediatR;

namespace DwapiCentral.Ct.Application.Commands.DifferentialCommands;

public class MergeDifferentialArtFastTrackCommand : IRequest<Result>
{
    public List<ARTFastTrackProfile>ArtFastTrackprofile { get; set; }

    public MergeDifferentialArtFastTrackCommand(List<ARTFastTrackProfile> patientProfiles)
    {
        ArtFastTrackprofile = patientProfiles;
    }
}

public class MergeDifferentialArtFastTrackCommandHandler : IRequestHandler<MergeDifferentialArtFastTrackCommand, Result>
{

    private readonly IArtFastTrackRepository _extractRepository;

    public MergeDifferentialArtFastTrackCommandHandler(IArtFastTrackRepository extractRepository)
    {
        _extractRepository = extractRepository;
    }

    public async Task<Result> Handle(MergeDifferentialArtFastTrackCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var extractsToUpdate = new List<ArtFastTrackExtract>();
            var extractsToInsert = new List<ArtFastTrackExtract>();

            foreach (var profile in request.ArtFastTrackprofile)
            {
                foreach (var extract in profile.ArtFastTrackExtracts)
                { // Check if the extract already exists in the database
                    var existingLabExtract = await _extractRepository.GetExtractByUniqueIdentifiers(
                        extract.PatientPk, extract.SiteCode, extract.RecordUUID);

                    if (existingLabExtract != null)
                    {
                        extractsToUpdate.Add(extract);
                    }
                    else
                    {
                        extractsToInsert.Add(extract);
                    }
                }
            }

            if (extractsToUpdate.Count > 0)
            {
                await _extractRepository.UpdateExtract(extractsToUpdate);
            }

            if (extractsToInsert.Count > 0)
            {
                await _extractRepository.InsertExtract(extractsToInsert);
            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }

    }
}




