using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Prep.Controllers
{
    public class PrepCareTerminationController : Controller
    {
        private readonly IMediator _mediator;


        public PrepCareTerminationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/PrepCareTermination")]
        public async Task<IActionResult> ProcessPrepCt(PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepCareTerminationCommand(extract.PrepCareTerminationExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepCareTermination error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergePrepCareTerminationCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
