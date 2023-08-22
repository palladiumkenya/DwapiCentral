using DwapiCentral.Hts.Application.Commands;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Hts.Controllers
{
    public class HtsClientController : Controller
    {

        private readonly IMediator _mediator;


        public HtsClientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/Hts/Clients
        [HttpPost("api/Hts/Clients")]
        public async Task<IActionResult> ProcessClient([FromBody] MergeHtsClientsCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
              var id = BackgroundJob.Enqueue(() => SaveClientsJob(client));
              
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

        [Queue("clients")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveClientsJob(MergeHtsClientsCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
