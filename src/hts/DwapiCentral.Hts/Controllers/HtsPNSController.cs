using DwapiCentral.Hts.Application.Commands;
using DwapiCentral.Hts.Domain.Events;
using DwapiCentral.Hts.Domain.Repository;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Hts.Controllers
{
    public class HtsPNSController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public HtsPNSController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }
        // POST api/Hts/Pns
        [HttpPost("api/Hts/Pns")]
        public async Task<IActionResult> ProcessPns([FromBody] MergeHtsPNSCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
               // var id = BackgroundJob.Enqueue(() => SavePNSJob(client));
                var id = BackgroundJob.Schedule(() => SavePNSJob(client), TimeSpan.FromSeconds(5));
                var manifestId = await _manifestRepository.GetManifestId(client.PartnerNotificationServices.FirstOrDefault().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = client.PartnerNotificationServices.Count(), ManifestId = manifestId, SiteCode = client.PartnerNotificationServices.First().SiteCode, ExtractName = "HtsPartnerNotificationServices" };

                await _mediator.Publish(notification);

                return Ok(new
                {
                    BatchKey = id
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("pns")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SavePNSJob(MergeHtsPNSCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
