using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Prep.Controllers
{
    public class PrepPharmacyController : Controller
    {
        private readonly IMediator _mediator;


        public PrepPharmacyController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("api/Mnch/PrepPharmacy")]
        public async Task<IActionResult> ProcessPrepPharmacy(PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepPharmacyCommand(extract.PrepPharmacyExtracts)));
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepPharmacy error");
                return StatusCode(500, e.Message);
            }
        }


        public async Task ProcessExtractCommand(MergePrepPharmacyCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
