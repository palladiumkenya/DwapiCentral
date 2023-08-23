using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class MnchLabController : Controller
    {
        private readonly IMediator _mediator;


        public MnchLabController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/MnchLab")]
        public async Task<IActionResult> ProcessMnchLab(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMnchLabCommand(extract.MnchLabExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "MnchLab error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeMnchLabCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
