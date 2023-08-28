using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Prep.Controllers
{
    public class PrepLabController : Controller
    {
        private readonly IMediator _mediator;


        public PrepLabController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/PrepLab")]
        public async Task<IActionResult> ProcessPrepLab(PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepLabCommand(extract.PrepLabExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepLab error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergePrepLabCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
