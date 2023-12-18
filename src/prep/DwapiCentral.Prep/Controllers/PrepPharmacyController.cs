using DwapiCentral.Contracts.Common;
using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.DTOs;
using DwapiCentral.Prep.Domain.Events;
using DwapiCentral.Prep.Domain.Repository;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Prep.Controllers
{
    public class PrepPharmacyController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public PrepPharmacyController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Prep/PrepPharmacy")]
        public async Task<IActionResult> ProcessPrepPharmacy([FromBody] PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {
                var id = BackgroundJob.Schedule(() => ProcessExtractCommand(new MergePrepPharmacyCommand(extract.PrepPharmacyExtracts)), TimeSpan.FromSeconds(5));
                //var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePrepPharmacyCommand(extract.PrepPharmacyExtracts)));
                var manifestId = await _manifestRepository.GetManifestId(extract.PrepPharmacyExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.PrepPharmacyExtracts.Count, ManifestId = manifestId, SiteCode = extract.PrepPharmacyExtracts.First().SiteCode, ExtractName = "PrepPharmacyExtract" };
                await _mediator.Publish(notification);
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PrepPharmacy error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("preppharmacy")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePrepPharmacyCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
