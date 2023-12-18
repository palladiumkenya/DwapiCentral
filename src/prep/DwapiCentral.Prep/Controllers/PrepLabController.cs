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
    public class PrepLabController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public PrepLabController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
           _manifestRepository= manifestRepository;
        }


        [HttpPost("api/Prep/PrepLab")]
        public async Task<IActionResult> ProcessPrepLab([FromBody] PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Schedule(() => ProcessExtractCommand(new MergePrepLabCommand(extract.PrepLabExtracts)), TimeSpan.FromSeconds(5));
               // var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepLabCommand(extract.PrepLabExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.PrepLabExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.PrepLabExtracts.Count, ManifestId = manifestId, SiteCode = extract.PrepLabExtracts.First().SiteCode, ExtractName = "PrepLabExtract" };
                await _mediator.Publish(notification);

                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepLab error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("preplab")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePrepLabCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
