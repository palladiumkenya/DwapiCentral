using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class MotherBabyPairController : Controller
    {
        private readonly IMediator _mediator;


        public MotherBabyPairController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/MotherBabyPair")]
        public async Task<IActionResult> ProcessMotherBabyPair(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMotherBabyPairCommand(extract.MotherBabyPairExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "MotherBabyPair error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeMotherBabyPairCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
