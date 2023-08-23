using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class MnchArtController : Controller
    {
        private readonly IMediator _mediator;


        public MnchArtController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/MnchArt")]
        public async Task<IActionResult> ProcessMnchArt(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMnchArtCommand(extract.MnchArtExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "MnchArt error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeMnchArtCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
