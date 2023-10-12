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
    public class CwcVisitController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;



        public CwcVisitController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/CwcVisit")]
        public async Task<IActionResult> ProcessCwcVisit([FromBody] MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Schedule(() => ProcessExtractCommand(new MergeCwcVisitCommand(extract.CwcVisitExtracts)), TimeSpan.FromSeconds(5));
              //  var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeCwcVisitCommand(extract.CwcVisitExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.CwcVisitExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.CwcVisitExtracts.Count, ManifestId = manifestId, SiteCode = extract.CwcVisitExtracts.First().SiteCode, ExtractName = "CwcVisitExtract" };
                await _mediator.Publish(notification);
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "CwcVisit error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("cwcvisit")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergeCwcVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
