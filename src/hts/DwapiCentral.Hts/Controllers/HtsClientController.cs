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
    public class HtsClientController : Controller
    {

        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;


        public HtsClientController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }

        // POST api/Hts/Clients
        [HttpPost("api/Hts/Clients")]
        public async Task<IActionResult> ProcessClient([FromBody] MergeHtsClientsCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                var id = BatchJob.StartNew(x =>
                    {
                        x.Enqueue(() => SaveClientsJob(client));
                    });
               
                var manifestId = await _manifestRepository.GetManifestId(client.Clients.FirstOrDefault().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = client.Clients.Count(), ManifestId = manifestId, SiteCode = client.Clients.First().SiteCode, ExtractName = "HtsClient" };

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

        [Queue("clients")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveClientsJob(MergeHtsClientsCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
