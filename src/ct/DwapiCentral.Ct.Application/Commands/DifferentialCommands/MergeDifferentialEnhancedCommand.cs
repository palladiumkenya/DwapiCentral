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

public class MergeDifferentialEnhancedCommand : IRequest<Result>
{

    public List<EnhancedAdherenceCounsellingProfile> Patientprofile { get; set; }

    public MergeDifferentialEnhancedCommand(List<EnhancedAdherenceCounsellingProfile> patientProfiles)
    {
        Patientprofile = patientProfiles;
    }
}

public class MergeDifferentialEnhancedCommandHandler : IRequestHandler<MergeDifferentialEnhancedCommand, Result>
{

    private readonly IEnhancedAdherenceCounsellingRepository _extractRepository;

    public MergeDifferentialEnhancedCommandHandler(IEnhancedAdherenceCounsellingRepository extractRepository)
    {
        _extractRepository = extractRepository;
    }

    public async Task<Result> Handle(MergeDifferentialEnhancedCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var extractsToUpdate = new List<EnhancedAdherenceCounsellingExtract>();
            var extractsToInsert = new List<EnhancedAdherenceCounsellingExtract>();

            foreach (var profile in request.Patientprofile)
            {
                foreach (var extract in profile.EnhancedAdherenceCounsellingExtracts)
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

