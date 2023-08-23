using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class PncVisitController : Controller
    {
        private readonly IMediator _mediator;


        public PncVisitController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/PncVisit")]
        public async Task<IActionResult> ProcessPncVisit(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePncVisitCommand(extract.PncVisitExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PncVisit error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergePncVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
