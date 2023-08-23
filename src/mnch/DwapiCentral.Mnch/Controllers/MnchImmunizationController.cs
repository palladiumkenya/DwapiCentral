using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class MnchImmunizationController : Controller
    {
        private readonly IMediator _mediator;


        public MnchImmunizationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/MnchImmunization")]
        public async Task<IActionResult> ProcessMnchImmunization(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMnchImmunizationCommand(extract.MnchImmunizationExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "MnchImmunization error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeMnchImmunizationCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
