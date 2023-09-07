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
    public class MnchImmunizationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public MnchImmunizationController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/MnchImmunization")]
        public async Task<IActionResult> ProcessMnchImmunization([FromBody] MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMnchImmunizationCommand(extract.MnchImmunizationExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.MnchImmunizationExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.MnchImmunizationExtracts.Count, ManifestId = manifestId, SiteCode = extract.MnchImmunizationExtracts.First().SiteCode, ExtractName = "MnchImmunizations" };
                await _mediator.Publish(notification);

                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "MnchImmunization error");
                return StatusCode(500, e.Message);
            }
        }


        [Queue("immunization")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergeMnchImmunizationCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
