using DwapiCentral.Hts.Application.Commands;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
               // var id = BackgroundJob.Enqueue(() => _htsService.Process(client.Clients));
              var id =  await _mediator.Send(client);
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
    }
}
