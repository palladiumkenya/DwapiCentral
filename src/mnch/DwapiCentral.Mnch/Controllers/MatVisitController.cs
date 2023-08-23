using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class MatVisitController : Controller
    {
        private readonly IMediator _mediator;


        public MatVisitController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/MatVisit")]
        public async Task<IActionResult> ProcessMatVisit(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMatVisitCommand(extract.MatVisitExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "Mat Visit error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeMatVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
