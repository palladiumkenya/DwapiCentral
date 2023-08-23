using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class MnchEnrolmentController : Controller
    {
        private readonly IMediator _mediator;


        public MnchEnrolmentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/MnchEnrolment")]
        public async Task<IActionResult> ProcessMnchEnrolment(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeMnchEnrolmentCommand(extract.MnchEnrolmentExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "MnchEnrolment error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeMnchEnrolmentCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
