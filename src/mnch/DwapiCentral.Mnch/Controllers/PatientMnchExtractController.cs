using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Mnch.Controllers
{
    public class PatientMnchExtractController : Controller
    {
        private readonly IMediator _mediator;


        public PatientMnchExtractController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/PatientMnch")]
        public async Task<IActionResult> ProcessPatientMnch(MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePatientMnchExtractCommand(extract.PatientMnchExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PatientMnch error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergePatientMnchExtractCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
