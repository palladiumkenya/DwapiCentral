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
    public class PncVisitController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public PncVisitController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;

        }


        [HttpPost("api/Mnch/PncVisit")]
        public async Task<IActionResult> ProcessPncVisit(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePncVisitCommand(extract.PncVisitExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.AncVisitExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extract.PncVisitExtracts.Count, ManifestId = manifestId, SiteCode = extract.PncVisitExtracts.First().SiteCode, ExtractName = "PncVisits" };
                await _mediator.Publish(notification);
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PncVisit error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("pncvisit")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePncVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
