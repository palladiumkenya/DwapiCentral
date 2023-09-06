using DwapiCentral.Contracts.Common;
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
    public class MnchEnrolmentController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public MnchEnrolmentController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/MnchEnrolment")]
        public async Task<IActionResult> ProcessMnchEnrolment(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMnchEnrolmentCommand(extract.MnchEnrolmentExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.AncVisitExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.MnchEnrolmentExtracts.Count, ManifestId = manifestId, SiteCode = extract.MnchEnrolmentExtracts.First().SiteCode, ExtractName = "MnchEnrolments" };
                await _mediator.Publish(notification);
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "MnchEnrolment error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("mnchenrollment")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergeMnchEnrolmentCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
