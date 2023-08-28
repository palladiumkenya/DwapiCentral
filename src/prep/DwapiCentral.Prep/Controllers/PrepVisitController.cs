using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Prep.Controllers
{
    public class PrepVisitController : Controller
    {
        private readonly IMediator _mediator;


        public PrepVisitController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/PrepVisit")]
        public async Task<IActionResult> ProcessPrepVisit(PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepVisitCommand(extract.PrepVisitExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepVisit error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergePrepVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
