﻿using CSharpFunctionalExtensions;
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

public class MergeDifferentialPharmacyCommand : IRequest<Result>
{
    public List<PatientPharmacyProfile> Patientprofile { get; set; }

    public MergeDifferentialPharmacyCommand(List<PatientPharmacyProfile> patientProfiles)
    {
        Patientprofile = patientProfiles;
    }
}

public class MergeDifferentialPharmacyCommandHandler : IRequestHandler<MergeDifferentialPharmacyCommand, Result>
{

    private readonly IPatientPharmacyRepository _extractRepository;

    public MergeDifferentialPharmacyCommandHandler(IPatientPharmacyRepository extractRepository)
    {
        _extractRepository = extractRepository;
    }

    public async Task<Result> Handle(MergeDifferentialPharmacyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var extractsToUpdate = new List<PatientPharmacyExtract>();
            var extractsToInsert = new List<PatientPharmacyExtract>();

            foreach (var profile in request.Patientprofile)
            {
                foreach (var extract in profile.PharmacyExtracts)
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




