using DwapiCentral.Hts.Application.Commands;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Hts.Controllers
{
    public class HtsClientLinkageController : Controller
    {

        private readonly IMediator _mediator;


        public HtsClientLinkageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/Hts/Linkages
        [HttpPost("api/Hts/Linkages")]
        public IActionResult ProcessLinkages([FromBody] MergeHtsClientLinkageCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                var id = BackgroundJob.Enqueue(() => SaveClientLinkageJob(client));

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

        [Queue("linkages")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveClientLinkageJob(MergeHtsClientLinkageCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
