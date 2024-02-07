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
    public class MotherBabyPairController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public MotherBabyPairController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Mnch/MotherBabyPair")]
        public async Task<IActionResult> ProcessMotherBabyPair([FromBody] MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Schedule(() => ProcessExtractCommand(new MergeMotherBabyPairCommand(extract.MotherBabyPairExtracts)), TimeSpan.FromSeconds(5));
               // var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMotherBabyPairCommand(extract.MotherBabyPairExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.MotherBabyPairExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.MotherBabyPairExtracts.Count, ManifestId = manifestId, SiteCode = extract.MotherBabyPairExtracts.First().SiteCode, ExtractName = "MotherBabyPairExtract" };
                await _mediator.Publish(notification);
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "MotherBabyPair error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("motherbabypair")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergeMotherBabyPairCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
