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

public class MergeDifferentialIIptCommand :IRequest<Result>
{
    public List<IptProfile> Patientprofile { get; set; }

    public MergeDifferentialIIptCommand(List<IptProfile> patientProfiles)
    {
        Patientprofile = patientProfiles;
    }
}

public class MergeDifferentialIIptCommandHandler : IRequestHandler<MergeDifferentialIIptCommand, Result>
{

    private readonly IIptRepository _extractRepository;

    public MergeDifferentialIIptCommandHandler(IIptRepository extractRepository)
    {
        _extractRepository = extractRepository;
    }

    public async Task<Result> Handle(MergeDifferentialIIptCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var extractsToUpdate = new List<IptExtract>();
            var extractsToInsert = new List<IptExtract>();

            foreach (var profile in request.Patientprofile)
            {
                foreach (var extract in profile.IptExtracts)
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



