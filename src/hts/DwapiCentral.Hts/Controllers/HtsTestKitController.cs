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
    public class HtsTestKitController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;

        public HtsTestKitController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        // POST api/Hts/HtsTestKits
        [HttpPost("api/Hts/HtsTestKits")]
        public async Task<IActionResult> ProcessKits([FromBody] MergeHtsTestKitCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                //var id = BackgroundJob.Enqueue(() => SaveTestKitJob(client));
                var id = BackgroundJob.Schedule(() => SaveTestKitJob(client), TimeSpan.FromSeconds(1));
                var manifestId = await _manifestRepository.GetManifestId(client.TestKits.FirstOrDefault().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = client.TestKits.Count(), ManifestId = manifestId, SiteCode = client.TestKits.First().SiteCode, ExtractName = "HtsTestKits" };

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

        [Queue("testkits")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveTestKitJob(MergeHtsTestKitCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
