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
    public class MnchArtController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public MnchArtController(IMediator mediator,IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/MnchArt")]
        public async Task<IActionResult> ProcessMnchArt([FromBody] MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Schedule(() => ProcessExtractCommand(new MergeMnchArtCommand(extract.MnchArtExtracts)), TimeSpan.FromSeconds(5));
                //var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMnchArtCommand(extract.MnchArtExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.MnchArtExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.MnchArtExtracts.Count, ManifestId = manifestId, SiteCode = extract.MnchArtExtracts.First().SiteCode, ExtractName = "MnchArtExtract" };
                await _mediator.Publish(notification);

                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "MnchArt error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("mnchart")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergeMnchArtCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
