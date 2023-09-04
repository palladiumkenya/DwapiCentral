using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Contracts.Common;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.Interfaces.profiles;
using DwapiCentral.Ct.Application.Interfaces.Repository;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Ct.Domain.Models;
using DwapiCentral.Ct.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Application.Commands.DifferentialCommands;

public class MergeDifferentialLabsCommand : IRequest<Result>
{
    public List<PatientLabProfile> Patientprofile { get; set; }


    public MergeDifferentialLabsCommand(List<PatientLabProfile> patientLabProfiles)
    {
        Patientprofile = patientLabProfiles;
    }
}

public class MergeDifferentialDataCommandHandler : IRequestHandler<MergeDifferentialLabsCommand, Result>
{

    private readonly IPatientLaboratoryExtractRepository _patientLabExtractRepository;


    public MergeDifferentialDataCommandHandler(IPatientLaboratoryExtractRepository patientLabExtractRepository)
    {
        _patientLabExtractRepository = patientLabExtractRepository;


    }

    public async Task<Result> Handle(MergeDifferentialLabsCommand request, CancellationToken cancellationToken)
    {
        try
        {


            var labExtractsToUpdate = new List<PatientLaboratoryExtract>();
            var labExtractsToInsert = new List<PatientLaboratoryExtract>();

            foreach (var profile in request.Patientprofile)
            {


                foreach (var labExtract in profile.LaboratoryExtracts)
                { // Check if the lab extract already exists in the database
                    var existingLabExtract = await _patientLabExtractRepository.GetPatientLabExtractByUniqueIdentifiers(
                        labExtract.PatientPk, labExtract.SiteCode, labExtract.RecordUUID);

                    if (existingLabExtract != null)
                    {
                        labExtractsToUpdate.Add(labExtract);
                    }
                    else
                    {

                        labExtractsToInsert.Add(labExtract);
                    }
                }
            }

            if (labExtractsToUpdate.Count > 0)
            {

                await _patientLabExtractRepository.UpdatePatientLabExtract(labExtractsToUpdate);


            }


            if (labExtractsToInsert.Count > 0)
            {


                await _patientLabExtractRepository.InsertPatientLabExtract(labExtractsToInsert);


            }

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }

    }
}


