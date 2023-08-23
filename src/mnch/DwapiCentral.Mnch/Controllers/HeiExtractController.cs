using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class HeiExtractController : Controller
    {
        private readonly IMediator _mediator;


        public HeiExtractController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/Hei")]
        public async Task<IActionResult> ProcessHei(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeHeiExtractCommand(extract.HeiExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "Hei error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeHeiExtractCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
