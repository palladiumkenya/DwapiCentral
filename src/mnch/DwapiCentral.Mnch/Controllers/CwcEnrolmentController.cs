using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class CwcEnrolmentController : Controller
    {
        private readonly IMediator _mediator;


        public CwcEnrolmentController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/CwcEnrolment")]
        public async Task<IActionResult> ProcessCwcEnrolment(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergeCwcEnrolmentCommand(extract.CwcEnrolmentExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "CwcEnrolment error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergeCwcEnrolmentCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
