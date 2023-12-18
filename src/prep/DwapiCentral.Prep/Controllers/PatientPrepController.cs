using DwapiCentral.Contracts.Common;
using DwapiCentral.Prep.Application.Commands;
using DwapiCentral.Prep.Application.DTOs;
using DwapiCentral.Prep.Domain.Events;
using DwapiCentral.Prep.Domain.Models;
using DwapiCentral.Prep.Domain.Repository;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Prep.Controllers
{
    public class PatientPrepController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public PatientPrepController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        [HttpPost("api/Prep/PatientPrep")]
        public async Task<IActionResult> ProcessPatientPrep([FromBody] PrepExtractsDto extract)
        {
            if (null == extract) return BadRequest();
            try
            {

                var id = BackgroundJob.Enqueue(() => ProcessExtractCommand(new MergePatientPrepCommand(extract.PatientPrepExtracts)));
               

                var manifestId = await _manifestRepository.GetManifestId(extract.PatientPrepExtracts.FirstOrDefault().SiteCode);
                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = extract.PatientPrepExtracts.Count, ManifestId = manifestId, SiteCode = extract.PatientPrepExtracts.First().SiteCode, ExtractName = "PatientPrepExtract" };
                await _mediator.Publish(notification);
                return Ok(new { BatchKey = id });
            }
            catch (Exception e)
            {
                Log.Error(e, "PatientPrep error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("patientprep")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task ProcessExtractCommand(MergePatientPrepCommand saveExtractCommand)
        {
            await _mediator.Send(saveExtractCommand);

        }
    }
}
