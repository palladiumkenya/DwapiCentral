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

public class MergeDifferentialAllergiesCommand : IRequest<Result>
{
    public List<AllergiesChronicIllnessProfile> Patientprofile { get; set; }


    public MergeDifferentialAllergiesCommand(List<AllergiesChronicIllnessProfile> patientLabProfiles)
    {
        Patientprofile = patientLabProfiles;
    }
}

public class MergeDifferentialAllergiesCommandHandler : IRequestHandler<MergeDifferentialAllergiesCommand, Result>
{

    private readonly IAllergiesChronicIllnessRepository _allergiesExtractRepository;


    public MergeDifferentialAllergiesCommandHandler(IAllergiesChronicIllnessRepository allergiesExtractRepository)
    {
        _allergiesExtractRepository = allergiesExtractRepository;


    }

    public async Task<Result> Handle(MergeDifferentialAllergiesCommand request, CancellationToken cancellationToken)
    {
        try
        {


            var extractsToUpdate = new List<AllergiesChronicIllnessExtract>();
            var extractsToInsert = new List<AllergiesChronicIllnessExtract>();

            foreach (var profile in request.Patientprofile)
            {


                foreach (var extract in profile.AllergiesChronicIllnessExtracts)
                { // Check if the lab extract already exists in the database
                    var existingLabExtract = await _allergiesExtractRepository.GetExtractByUniqueIdentifiers(
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

                await _allergiesExtractRepository.UpdateExtract(extractsToUpdate);


            }


            if (extractsToInsert.Count > 0)
            {


                await _allergiesExtractRepository.InsertExtract(extractsToInsert);


            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }

    }
}
