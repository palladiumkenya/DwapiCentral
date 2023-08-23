using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class AncVisitController : Controller
    {
        private readonly IMediator _mediator;


        public AncVisitController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/AncVisit")]
        public async Task<IActionResult> ProcessAncVisit(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                
                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeAncVisitCommand(extract.AncVisitExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "AncVisit error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeAncVisitCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
