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
    public class HtsEligibilityScreeningController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public HtsEligibilityScreeningController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }
        // POST api/Hts/HtsEligibility
        [HttpPost("api/Hts/HtsEligibilityScreening")]
        public async Task<IActionResult> ProcessHtsEligibility([FromBody] MergeHtsEligibilityScreeningCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                //var id = BackgroundJob.Enqueue(() => SaveHtsEligibilityScreeningJob(client));
                var id = BackgroundJob.Schedule(() => SaveHtsEligibilityScreeningJob(client), TimeSpan.FromSeconds(20));
                var manifestId = await _manifestRepository.GetManifestId(client.HtsEligibility.FirstOrDefault().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = client.HtsEligibility.Count(), ManifestId = manifestId, SiteCode = client.HtsEligibility.First().SiteCode, ExtractName = "HtsEligibilityExtract" };

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

        [Queue("eligibility")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveHtsEligibilityScreeningJob(MergeHtsEligibilityScreeningCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
