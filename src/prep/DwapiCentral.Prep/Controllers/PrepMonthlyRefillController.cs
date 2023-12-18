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
    public class PrepMonthlyRefillController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public PrepMonthlyRefillController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Prep/PrepMonthlyRefill")]
        public async Task<IActionResult> ProcessPrepMonthlyRefills([FromBody] PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Schedule(() => ProcessExtractCommand(new MergePrepMonthlyRefillCommand(extract.PrepMonthlyRefillExtracts)), TimeSpan.FromSeconds(5));
               
                var manifestId = await _manifestRepository.GetManifestId(extract.PrepMonthlyRefillExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.PrepMonthlyRefillExtracts.Count, ManifestId = manifestId, SiteCode = extract.PrepMonthlyRefillExtracts.First().SiteCode, ExtractName = "PrepMonthlyRefills" };
                await _mediator.Publish(notification);
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepMonthlyRefills error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("prepmonthlyrefills")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePrepMonthlyRefillCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}