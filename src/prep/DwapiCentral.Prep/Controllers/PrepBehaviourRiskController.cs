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
    public class PrepBehaviourRiskController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public PrepBehaviourRiskController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/PrepBehaviourRisk")]
        public async Task<IActionResult> ProcessPrepBehaviourRisk(PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepBehaviourRiskCommand(extract.PrepBehaviourRiskExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.PrepBehaviourRiskExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.PrepBehaviourRiskExtracts.Count, ManifestId = manifestId, SiteCode = extract.PrepBehaviourRiskExtracts.First().SiteCode, ExtractName = "PrepBehaviourRisks" };
                await _mediator.Publish(notification);
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepBehaviourRisk error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("prepbehaviour")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePrepBehaviourRiskCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
