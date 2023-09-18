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
    public class PrepAdverseEventController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public PrepAdverseEventController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Prep/PrepAdverseEvent")]
        public async Task<IActionResult> ProcessPrepAdverseEvent( [FromBody] PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepAdverseEventCommand(extract.PrepAdverseEventExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.PrepAdverseEventExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.PrepAdverseEventExtracts.Count, ManifestId = manifestId, SiteCode = extract.PrepAdverseEventExtracts.First().SiteCode, ExtractName = "PrepAdverseEventExtract" };
                await _mediator.Publish(notification);

                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepAdverseEvent error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("prepadversevent")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePrepAdverseEventCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
