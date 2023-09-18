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
    public class HtsPartnerTracingController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public HtsPartnerTracingController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        // POST api/Hts/HtsPartnerTracings
        [HttpPost("api/Hts/HtsPartnerTracings")]
        public async Task<IActionResult> ProcessTracings([FromBody] MergeHtsPartnerTracingCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                 var id = BackgroundJob.Enqueue(() => SavePartnerTracingJob(client));

                var manifestId = await _manifestRepository.GetManifestId(client.PartnerTracing.FirstOrDefault().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = client.PartnerTracing.Count(), ManifestId = manifestId, SiteCode = client.PartnerTracing.First().SiteCode, ExtractName = "HtsPartnerTracing" };

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

        [Queue("partnertracing")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SavePartnerTracingJob(MergeHtsPartnerTracingCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
