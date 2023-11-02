using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Domain.Models.Stage;
using DwapiCentral.Ct.Domain.Repository.Stage;
using MediatR;

namespace DwapiCentral.Ct.Application.Commands;

public class MergeArtFastTrackCommand : IRequest<Result>
{

    public ArtFastTrackSourceBag ArtFastTrackSourceBag { get; set; }

    public MergeArtFastTrackCommand(ArtFastTrackSourceBag artFastTrackSourceBag)
    {
        ArtFastTrackSourceBag = artFastTrackSourceBag;
    }

}

public class MergeArtFastTrackCommandHandler : IRequestHandler<MergeArtFastTrackCommand, Result>
{
    private readonly IStageArtFastTrackExtractRepository _stageRepository;
    private readonly IMapper _mapper;

    public MergeArtFastTrackCommandHandler(IStageArtFastTrackExtractRepository stageArtFastTrackRepository, IMapper mapper)
    {
        _stageRepository = stageArtFastTrackRepository;
        _mapper = mapper;
    }

    public async Task<Result> Handle(MergeArtFastTrackCommand request, CancellationToken cancellationToken)
    {

        try
        {
            //await _allergiesChronicIllnessRepository.MergeAsync(request.AllergiesChronicIllnessExtracts);
            var extracts = _mapper.Map<List<StageArtFastTrackExtract>>(request.ArtFastTrackSourceBag.Extracts);
            if (extracts.Any())
            {
                StandardizeClass<StageArtFastTrackExtract, ArtFastTrackSourceBag> standardizer = new(extracts, request.ArtFastTrackSourceBag);
                standardizer.StandardizeExtracts();

            }
            //stage
            await _stageRepository.SyncStage(extracts, request.ArtFastTrackSourceBag.ManifestId.Value);
            return Result.Success();

        }
        catch(Exception ex)
        {
            return Result.Failure(ex.Message);
        }

    }
}

