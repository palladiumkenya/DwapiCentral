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
    public class HtsClientTestsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IManifestRepository _manifestRepository;



        public HtsClientTestsController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
        }


        // POST api/Hts/HtsClientTests
        [HttpPost("api/Hts/HtsClientTests")]
        public async Task<IActionResult> ProcessTests([FromBody] MergeHtsClientTestCommand client)
        {
            if (null == client)
                return BadRequest();

            try
            {
                var id  = BackgroundJob.Schedule(() => SaveClientTestsJob(client), TimeSpan.FromSeconds(5));
                //var id = BackgroundJob.Enqueue(() => SaveClientTestsJob(client));
               
                var manifestId = await _manifestRepository.GetManifestId(client.ClientTests.FirstOrDefault().SiteCode);

                var notification = new ExtractsReceivedEvent { TotalExtractsStaged = client.ClientTests.Count(), ManifestId = manifestId, SiteCode = client.ClientTests.First().SiteCode, ExtractName = "HtsClientTests" };

                await _mediator.Publish(notification);

                return Ok(new
                {
                    BatchKey = manifestId
                });
            }
            catch (Exception e)
            {
                Log.Error(e, "manifest error");
                return StatusCode(500, e.Message);
            }
        }

        [Queue("clienttests")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveClientTestsJob(MergeHtsClientTestCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }
    }
}
