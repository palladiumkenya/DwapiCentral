﻿using AutoMapper;
using CSharpFunctionalExtensions;
using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Application.Commands.DifferentialCommands;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Application.Profiles;
using DwapiCentral.Ct.Domain.Events;
using DwapiCentral.Shared.Custom;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Ct.Controllers
{
    public class ContactListingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ContactListingController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }


        [HttpPost]
        [Route("api/v3/ContactListing")]
        public async Task<IActionResult> PostBatch([FromBody] ContactListingSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    string jobId;
                    if (sourceBag.HasJobId)
                    {

                        jobId = BatchJob.ContinueBatchWith(sourceBag.JobId,
                          x => { x.Enqueue(() => Send($"{sourceBag}", new MergeContactListingCommand(sourceBag))); }, $"{sourceBag}");
                    }
                    else
                    {
                        jobId = BatchJob.StartNew(x =>
                        {
                            x.Enqueue(() => Send($"{sourceBag}", new MergeContactListingCommand(sourceBag)));
                        }, $"{sourceBag}");
                    }


                    var notification = new ExtractsReceivedEvent { TotalExtractsStaged = sourceBag.Extracts.Count, ManifestId = sourceBag.ManifestId, SiteCode = sourceBag.Extracts.First().SiteCode, ExtractName = "ContactListingExtract" };

                    await _mediator.Publish(notification);

                    var successMessage = new
                    {
                        JobId = jobId,
                        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                    };
                    return Ok(successMessage);
                    
                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(ContactListingSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new ContactListingSourceBag().GetType().Name}' is null");
        }


        [HttpPost]
        [Route("api/v2/ContactListing")]
        public async Task<IActionResult> PostBatchDiff([FromBody] List<ContactListingProfile> patientProfile)
        {
            if (null != patientProfile && patientProfile.Any())
            {
                try
                {
                    BackgroundJob.Enqueue(() => SaveDiffData(new MergeDifferentialContactListingCommand(patientProfile)));


                    var successMessage = new
                    {
                        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                    };
                    return Ok(successMessage);
                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(ContactListingProfile), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }
            return BadRequest($"The expected '{new ContactListingProfile().GetType().Name}' is null");
        }


        public async Task SaveDiffData(MergeDifferentialContactListingCommand saveDiffCommand)
        {
            await _mediator.Send(saveDiffCommand);

        }

        [Queue("contactlisting")]       
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task Send(string jobName, IRequest<Result> command)
        {
            await _mediator.Send(command);
        }
    }
}
