using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Prep.Controllers
{
    public class PrepAdverseEventController : Controller
    {
        private readonly IMediator _mediator;


        public PrepAdverseEventController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/PrepAdverseEvent")]
        public async Task<IActionResult> ProcessPrepAdverseEvent(PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepAdverseEventCommand(extract.PrepAdverseEventExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepAdverseEvent error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergePrepAdverseEventCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
