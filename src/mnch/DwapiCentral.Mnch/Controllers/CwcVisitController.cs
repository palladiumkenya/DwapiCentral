using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class CwcVisitController : Controller
    {
        private readonly IMediator _mediator;


        public CwcVisitController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/CwcVisit")]
        public async Task<IActionResult> ProcessCwcVisit(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeCwcVisitCommand(extract.CwcVisitExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "CwcVisit error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeCwcVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
