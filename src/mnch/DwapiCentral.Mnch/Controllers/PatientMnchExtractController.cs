using DwapiCentral.Contracts.Common;
using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using DwapiCentral.Mnch.Domain.Events;
using DwapiCentral.Mnch.Domain.Repository;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Mnch.Controllers
{
    public class PatientMnchExtractController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public PatientMnchExtractController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost]
        [Route("api/Mnch/PatientMnch")]
        public async Task<IActionResult> ProcessPatientMnch([FromBody] MnchExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePatientMnchExtractCommand(extract.PatientMnchExtracts)));

                var manifestId = await _manifestRepository.GetManifestId(extract.PatientMnchExtracts.FirstOrDefault().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.PatientMnchExtracts.Count, ManifestId = manifestId, SiteCode = extract.PatientMnchExtracts.First().SiteCode, ExtractName = "PatientMnchExtract" };

                await _mediator.Publish(notification);

                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "PatientMnch error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("patientmnch")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePatientMnchExtractCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
