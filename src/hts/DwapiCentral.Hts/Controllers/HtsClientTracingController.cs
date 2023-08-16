using DwapiCentral.Hts.Application.Commands;
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


        public HtsClientTracingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/Hts/HtsClientTracings
        [HttpPost("api/Hts/HtsClientTracings")]
        public IActionResult ProcessTracings([FromBody] MergeHtsClientTracingCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                var id = BackgroundJob.Enqueue(() => SaveClientTracingJob(client));

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
