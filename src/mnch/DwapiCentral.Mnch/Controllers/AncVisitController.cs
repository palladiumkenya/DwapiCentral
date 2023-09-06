using DwapiCentral.Contracts.Common;
using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using DwapiCentral.Mnch.Domain.Events;
using DwapiCentral.Mnch.Domain.Repository;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Mnch.Controllers
{
    public class AncVisitController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public AncVisitController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/AncVisit")]
        public async Task<IActionResult> ProcessAncVisit(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                
                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeAncVisitCommand(extract.AncVisitExtracts)));

                var manifestId = await _manifestRepository.GetManifestId(extract.AncVisitExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.AncVisitExtracts.Count, ManifestId = manifestId, SiteCode = extract.AncVisitExtracts.First().SiteCode, ExtractName = "AncVisits" };
                await _mediator.Publish(notification);

                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "AncVisit error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("ancvisit")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergeAncVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
