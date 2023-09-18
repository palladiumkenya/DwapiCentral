using DwapiCentral.Hts.Application.Commands;
using DwapiCentral.Hts.Domain.Events;
using DwapiCentral.Hts.Domain.Repository;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Hts.Controllers
{
    public class HtsClientTracingController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public HtsClientTracingController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        // POST api/Hts/HtsClientTracings
        [HttpPost("api/Hts/HtsClientTracings")]
        public async Task<IActionResult> ProcessTracings([FromBody] MergeHtsClientTracingCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                var id = BackgroundJob.Enqueue(() => SaveClientTracingJob(client));
                
                var manifestId = await _manifestRepository.GetManifestId(client.ClientTracing.FirstOrDefault().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = client.ClientTracing.Count(), ManifestId = manifestId, SiteCode = client.ClientTracing.First().SiteCode, ExtractName = "HtsClientTracingExtract" };

                await _mediator.Publish(notification);

                return Ok(new
                {
                    BatchKey = id
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("clienttracings")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveClientTracingJob(MergeHtsClientTracingCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
