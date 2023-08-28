using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Prep.Controllers
{
    public class PatientPrepController : Controller
    {
        private readonly IMediator _mediator;


        public PatientPrepController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/PatientPrep")]
        public async Task<IActionResult> ProcessPatientPrep(PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePatientPrepCommand(extract.PatientPrepExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PatientPrep error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergePatientPrepCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
