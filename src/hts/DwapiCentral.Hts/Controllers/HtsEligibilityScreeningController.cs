using DwapiCentral.Hts.Application.Commands;
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


        public HtsEligibilityScreeningController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // POST api/Hts/HtsEligibility
        [HttpPost("api/Hts/HtsEligibilityScreening")]
        public IActionResult ProcessHtsEligibility([FromBody] MergeHtsEligibilityScreeningCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                var id = BackgroundJob.Enqueue(() => SaveHtsEligibilityScreeningJob(client));

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
