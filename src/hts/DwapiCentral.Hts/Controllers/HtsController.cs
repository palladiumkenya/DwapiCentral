using CSharpFunctionalExtensions;
using DwapiCentral.Hts.Application.Commands;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Hts.Controllers
{
   
    public class HtsController : Controller
    {

        private readonly IMediator _mediator;


        public HtsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/Hts/verify")]
        public async Task<IActionResult> Verify([FromBody] VerifySubscriber subscriber)
        {
            if (null == subscriber)
                return BadRequest();

            try
            {
                var dockect = await _mediator.Send(subscriber, HttpContext.RequestAborted);
                return Ok(dockect);
            }
            catch (Exception e)
            {
                Log.Error(e, "verify error");
                return StatusCode(500, e.Message);
            }
        }

        
        [HttpPost("api/Hts/Manifest")]
        public async Task<IActionResult> ProcessManifest([FromBody] SaveManifestCommand manifest)
        {
            if (null == manifest)
                return BadRequest();
            var validFacility = await _mediator.Send(new ValidateSiteCommand(manifest.manifest.SiteCode, manifest.manifest.Name));
            if (validFacility.IsSuccess)
            {
                string json = manifest.manifest.Cargoes[1].Items;

                dynamic data = JsonConvert.DeserializeObject(json);

                manifest.manifest.EmrVersion = data.EmrVersion;

                dynamic dwapiVersiondata = JsonConvert.DeserializeObject(manifest.manifest.Cargoes[2].Items);

                manifest.manifest.DwapiVersion = dwapiVersiondata.Version;

                try
                {
                  BackgroundJob.Enqueue(() => SaveManifestJob(manifest));
                 
                    return Ok();
                }
                catch (Exception e)
                {
                    Log.Error(e, "manifest error");
                    return StatusCode(500, e.Message);
                }
            }
            else return BadRequest(validFacility.Error.ToString());
        }

        [Queue("manifest")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveManifestJob(SaveManifestCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);
            
        }
    }
}
