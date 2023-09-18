using DwapiCentral.Contracts.Common;
using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.DTOs;
using DwapiCentral.Prep.Domain.Events;
using DwapiCentral.Prep.Domain.Repository;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Prep.Controllers
{
    public class PrepVisitController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public PrepVisitController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Prep/PrepVisit")]
        public async Task<IActionResult> ProcessPrepVisit([FromBody] PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepVisitCommand(extract.PrepVisitExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.PrepVisitExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.PrepVisitExtracts.Count, ManifestId = manifestId, SiteCode = extract.PrepVisitExtracts.First().SiteCode, ExtractName = "PrepVisitExtract" };
                await _mediator.Publish(notification);

                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepVisit error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("prepvisit")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePrepVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
