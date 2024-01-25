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
    public class HtsClientLinkageController : Controller
    {

        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public HtsClientLinkageController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        // POST api/Hts/Linkages
        [HttpPost("api/Hts/Linkages")]
        public async Task<IActionResult> ProcessLinkages([FromBody] MergeHtsClientLinkageCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
               // var id = BackgroundJob.Enqueue(() => SaveClientLinkageJob(client));
                var id = BackgroundJob.Schedule(() => SaveClientLinkageJob(client), TimeSpan.FromSeconds(5));
                var manifestId = await _manifestRepository.GetManifestId(client.ClientLinkage.FirstOrDefault().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = client.ClientLinkage.Count(), ManifestId = manifestId, SiteCode = client.ClientLinkage.First().SiteCode, ExtractName = "HtsClientLinkage" };

                await _mediator.Publish(notification);


                return Ok(new
                {
                    BatchKey = id
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "HtsClientLinkage error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("linkages")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveClientLinkageJob(MergeHtsClientLinkageCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
