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

public class MergeDifferentialCervicalCommand : IRequest<Result>
{
    public List<CervicalCancerScreeningProfile> Patientprofile { get; set; }


    public MergeDifferentialCervicalCommand(List<CervicalCancerScreeningProfile> patientLabProfiles)
    {
        Patientprofile = patientLabProfiles;
    }
}

public class MergeDifferentialCervicalCommandHandler : IRequestHandler<MergeDifferentialCervicalCommand, Result>
{

    private readonly ICervicalCancerScreeningRepository _cancerScreeningxtractRepository;


    public MergeDifferentialCervicalCommandHandler(ICervicalCancerScreeningRepository cancerScreeningxtractRepository)
    {
        _cancerScreeningxtractRepository = cancerScreeningxtractRepository;


    }

    public async Task<Result> Handle(MergeDifferentialCervicalCommand request, CancellationToken cancellationToken)
    {
        try
        {


            var extractsToUpdate = new List<CervicalCancerScreeningExtract>();
            var extractsToInsert = new List<CervicalCancerScreeningExtract>();

            foreach (var profile in request.Patientprofile)
            {


                foreach (var extract in profile.CervicalCancerScreeningExtracts)
                { // Check if the lab extract already exists in the database
                    var existingLabExtract = await _cancerScreeningxtractRepository.GetExtractByUniqueIdentifiers(
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

                await _cancerScreeningxtractRepository.UpdateExtract(extractsToUpdate);


            }


            if (extractsToInsert.Count > 0)
            {


                await _cancerScreeningxtractRepository.InsertExtract(extractsToInsert);


            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }

    }
}

