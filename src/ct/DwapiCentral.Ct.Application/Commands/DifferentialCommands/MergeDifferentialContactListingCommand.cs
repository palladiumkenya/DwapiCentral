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

public class MergeDifferentialContactListingCommand : IRequest<Result>
{

    public List<ContactListingProfile> Patientprofile { get; set; }


    public MergeDifferentialContactListingCommand(List<ContactListingProfile> patientLabProfiles)
    {
        Patientprofile = patientLabProfiles;
    }
}

public class MergeDifferentialContactListingCommandHandler : IRequestHandler<MergeDifferentialContactListingCommand, Result>
{

    private readonly IContactListingRepository _extractRepository;


    public MergeDifferentialContactListingCommandHandler(IContactListingRepository extractRepository)
    {
        _extractRepository = extractRepository;


    }

    public async Task<Result> Handle(MergeDifferentialContactListingCommand request, CancellationToken cancellationToken)
    {
        try
        {


            var extractsToUpdate = new List<ContactListingExtract>();
            var extractsToInsert = new List<ContactListingExtract>();

            foreach (var profile in request.Patientprofile)
            {


                foreach (var extract in profile.ContactListingExtracts)
                { // Check if the lab extract already exists in the database
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


