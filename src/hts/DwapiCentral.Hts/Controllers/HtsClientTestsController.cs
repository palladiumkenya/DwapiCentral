using DwapiCentral.Hts.Application.Commands;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Hts.Controllers
{
    public class HtsClientTestsController : Controller
    {
        private readonly IMediator _mediator;


        public HtsClientTestsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // POST api/Hts/HtsClientTests
        [HttpPost("api/Hts/HtsClientTests")]
        public IActionResult ProcessTests([FromBody] MergeHtsClientTestCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                var id = BackgroundJob.Enqueue(() => SaveClientTestsJob(client));

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

        [Queue("clienttests")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveClientTestsJob(MergeHtsClientTestCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
