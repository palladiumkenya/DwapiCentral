using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands.DifferentialCommands;

public class MergeDifferentialArtCommand : IRequest<Result>
{
    public List<PatientARTProfile> Patientprofile { get; set; }

    public MergeDifferentialArtCommand(List<PatientARTProfile> patientProfiles)
    {
        Patientprofile = patientProfiles;
    }
}

public class MergeDifferentialArtCommandHandler : IRequestHandler<MergeDifferentialArtCommand, Result>
{

    private readonly IPatientArtExtractRepository _extractRepository;

    public MergeDifferentialArtCommandHandler(IPatientArtExtractRepository extractRepository)
    {
        _extractRepository = extractRepository;
    }

    public async Task<Result> Handle(MergeDifferentialArtCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var extractsToUpdate = new List<PatientArtExtract>();
            var extractsToInsert = new List<PatientArtExtract>();

            foreach (var profile in request.Patientprofile)
            {
                foreach (var extract in profile.ArtExtracts)
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




