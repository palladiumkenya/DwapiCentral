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
    public class HeiExtractController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public HeiExtractController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/Hei")]
        public async Task<IActionResult> ProcessHei([FromBody] MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeHeiExtractCommand(extract.HeiExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.HeiExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsProcessed = extract.HeiExtracts.Count, ManifestId = manifestId, SiteCode = extract.HeiExtracts.First().SiteCode, ExtractName = "Heis" };
                await _mediator.Publish(notification);

                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "Hei error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("hei")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergeHeiExtractCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
