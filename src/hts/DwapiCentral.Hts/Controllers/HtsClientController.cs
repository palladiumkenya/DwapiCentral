using DwapiCentral.Hts.Application.Commands;
using DwapiCentral.Hts.Domain.Events;
using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Hts.Domain.Repository.Stage;
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
        private readonly IStageHtsClientRepository _stageRepository;


        public HtsClientController(IMediator mediator, IManifestRepository manifestRepository, IStageHtsClientRepository stageHtsClientRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
            _stageRepository = stageHtsClientRepository;
        }

        // POST api/Hts/Clients
        [HttpPost("api/Hts/Clients")]
        public async Task<IActionResult> ProcessClient([FromBody] MergeHtsClientsCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {

                //satge Data


                var id = BatchJob.StartNew(x =>
                    {
                        x.Enqueue(() => SaveClientsJob(client));
                    });

                //var id = BackgroundJob.Enqueue(() => SaveClientsJob(client));

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
