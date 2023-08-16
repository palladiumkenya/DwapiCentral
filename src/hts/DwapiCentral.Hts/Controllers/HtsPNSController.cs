using DwapiCentral.Hts.Application.Commands;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Hts.Controllers
{
    public class HtsPNSController : Controller
    {
        private readonly IMediator _mediator;


        public HtsPNSController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // POST api/Hts/Pns
        [HttpPost("api/Hts/Pns")]
        public IActionResult ProcessPns([FromBody] MergeHtsPNSCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                var id = BackgroundJob.Enqueue(() => SavePNSJob(client));

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

        [Queue("pns")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SavePNSJob(MergeHtsPNSCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
