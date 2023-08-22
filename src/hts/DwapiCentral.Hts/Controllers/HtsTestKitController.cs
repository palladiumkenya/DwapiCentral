using DwapiCentral.Hts.Application.Commands;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Hts.Controllers
{
    public class HtsTestKitController : Controller
    {
        private readonly IMediator _mediator;


        public HtsTestKitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/Hts/HtsTestKits
        [HttpPost("api/Hts/HtsTestKits")]
        public IActionResult ProcessKits([FromBody] MergeHtsTestKitCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                var id = BackgroundJob.Enqueue(() => SaveTestKitJob(client));

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

        [Queue("testkits")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveTestKitJob(MergeHtsTestKitCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
