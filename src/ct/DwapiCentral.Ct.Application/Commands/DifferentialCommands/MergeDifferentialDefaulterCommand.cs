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

public class MergeDifferentialDefaulterCommand : IRequest<Result>
{
    public List<DefaulterTracingProfile> Patientprofile { get; set; }

    public MergeDifferentialDefaulterCommand(List<DefaulterTracingProfile> patientProfiles)
    {
        Patientprofile = patientProfiles;
    }
}

public class MergeDifferentialDefaulterCommandHandler : IRequestHandler<MergeDifferentialDefaulterCommand, Result>
{

    private readonly IDefaulterTracingRepository _extractRepository;

    public MergeDifferentialDefaulterCommandHandler(IDefaulterTracingRepository extractRepository)
    {
        _extractRepository = extractRepository;
    }

    public async Task<Result> Handle(MergeDifferentialDefaulterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var extractsToUpdate = new List<DefaulterTracingExtract>();
            var extractsToInsert = new List<DefaulterTracingExtract>();

            foreach (var profile in request.Patientprofile)
            {
                foreach (var extract in profile.DefaulterTracingExtracts)
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

