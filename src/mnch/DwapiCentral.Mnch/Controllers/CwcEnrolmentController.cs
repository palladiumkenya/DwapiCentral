﻿using DwapiCentral.Contracts.Common;
using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using DwapiCentral.Mnch.Domain.Events;
using DwapiCentral.Mnch.Domain.Repository;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Mnch.Controllers
{
    public class CwcEnrolmentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public CwcEnrolmentController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/CwcEnrolment")]
        public async Task<IActionResult> ProcessCwcEnrolment(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeCwcEnrolmentCommand(extract.CwcEnrolmentExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.AncVisitExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extract.CwcEnrolmentExtracts.Count, ManifestId = manifestId, SiteCode = extract.CwcEnrolmentExtracts.First().SiteCode, ExtractName = "CwcEnrolments" };
                await _mediator.Publish(notification);

                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "CwcEnrolment error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("cwcenrollment")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergeCwcEnrolmentCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
