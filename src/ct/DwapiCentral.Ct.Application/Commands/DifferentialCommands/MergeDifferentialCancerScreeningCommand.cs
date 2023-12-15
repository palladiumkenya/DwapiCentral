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

namespace DwapiCentral.Ct.Application.Commands.DifferentialCommands
{
    public class MergeDifferentialCancerScreeningCommand : IRequest<Result>
    {
        public List<CancerScreeningProfile> Patientprofile { get; set; }


        public MergeDifferentialCancerScreeningCommand(List<CancerScreeningProfile> patientLabProfiles)
        {
            Patientprofile = patientLabProfiles;
        }
    }

    public class MergeDifferentialCancerScreeningCommandHandler : IRequestHandler<MergeDifferentialCancerScreeningCommand, Result>
    {

        private readonly ICancerScreeningRepository _cancerScreeningxtractRepository;


        public MergeDifferentialCancerScreeningCommandHandler(ICancerScreeningRepository cancerScreeningxtractRepository)
        {
            _cancerScreeningxtractRepository = cancerScreeningxtractRepository;


        }

        public async Task<Result> Handle(MergeDifferentialCancerScreeningCommand request, CancellationToken cancellationToken)
        {
            try
            {


                var extractsToUpdate = new List<CancerScreeningExtract>();
                var extractsToInsert = new List<CancerScreeningExtract>();

                foreach (var profile in request.Patientprofile)
                {


                    foreach (var extract in profile.CancerScreeningExtracts)
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
}
