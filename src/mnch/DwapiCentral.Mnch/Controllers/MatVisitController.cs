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
    public class MatVisitController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public MatVisitController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/MatVisit")]
        public async Task<IActionResult> ProcessMatVisit([FromBody] MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMatVisitCommand(extract.MatVisitExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.MatVisitExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.MatVisitExtracts.Count, ManifestId = manifestId, SiteCode = extract.MatVisitExtracts.First().SiteCode, ExtractName = "MatVisitExtract" };
                await _mediator.Publish(notification);


                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "Mat Visit error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("matvisit")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergeMatVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
