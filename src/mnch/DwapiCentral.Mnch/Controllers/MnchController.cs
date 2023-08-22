using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel;

namespace DwapiCentral.Mnch.Controllers
{
    public class MnchController : Controller
    {
        private readonly IMediator _mediator;


        public MnchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/Mnch/verify")]
        public async Task<IActionResult> Verify([FromBody] VerifySubscriber subscriber)
        {
            if (null == subscriber)
                return BadRequest();

            try
            {
                var dockect = await _mediator.Send(new VerifySubscriber(subscriber.SubscriberId, subscriber.AuthToken), HttpContext.RequestAborted);
                return Ok(dockect);
            }
            catch (Exception e)
            {
                Log.Error(e, "verify error");
                return StatusCode(500, e.Message);
            }
            
        }


        [HttpPost("api/Mnch/Manifest")]
        public async Task<IActionResult> ProcessManifest([FromBody] ManifestExtractDto manifestDto)
        {
            if (null == manifestDto)
                return BadRequest();
            var validFacility = await _mediator.Send(new ValidateSiteCommand(manifestDto.Manifest.SiteCode, manifestDto.Manifest.Name));
            if (validFacility.IsSuccess)
            {
                string json = manifestDto.Manifest.Cargoes[1].Items;

                dynamic data = JsonConvert.DeserializeObject(json);

                manifestDto.Manifest.EmrVersion = data.EmrVersion;

                dynamic dwapiVersiondata = JsonConvert.DeserializeObject(manifestDto.Manifest.Cargoes[2].Items);

                manifestDto.Manifest.DwapiVersion = dwapiVersiondata.Version;

                try
                {
                    var manifest = new SaveManifestCommand(manifestDto.Manifest);

                    var faciliyKey = await _mediator.Send(manifest, HttpContext.RequestAborted);

                    BackgroundJob.Enqueue(() => SaveManifestJob(new ProcessManifestCommand(manifestDto.Manifest.SiteCode)));
                    return Ok(new
                    {
                        FacilityKey = faciliyKey
                    });
                }
                catch (Exception e)
                {
                    Log.Error(e, "manifest error");
                    return StatusCode(500, e.Message);
                }
            }
            else return BadRequest(validFacility.Error.ToString());
        }

      
        public async Task SaveManifestJob(ProcessManifestCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }

    }
}
