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
    public class PrepCareTerminationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public PrepCareTerminationController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;

        }


        [HttpPost("api/Prep/PrepCareTermination")]
        public async Task<IActionResult> ProcessPrepCt([FromBody] PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepCareTerminationCommand(extract.PrepCareTerminationExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.PrepCareTerminationExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.PrepCareTerminationExtracts.Count, ManifestId = manifestId, SiteCode = extract.PrepCareTerminationExtracts.First().SiteCode, ExtractName = "PrepCareTerminationExtract" };
                await _mediator.Publish(notification);

                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepCareTermination error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("prepcaretermination")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePrepCareTerminationCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
