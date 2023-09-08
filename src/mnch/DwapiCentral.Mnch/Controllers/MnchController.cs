using DwapiCentral.Mnch.Application.Commands;
using DwapiCentral.Mnch.Application.DTOs;
using DwapiCentral.Mnch.Application.Events;
using DwapiCentral.Mnch.Domain.Repository;
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
        private readonly IManifestRepository _manifestRepository;


        public MnchController(IMediator mediator, IManifestRepository manifestRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
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

        [HttpPost]
        [Route("api/Mnch/Handshake")]
        public async Task<IActionResult> Post(Guid session)
        {
            try
            {
                var responce = await _mediator.Send(new ProcessHandshakeCommand(session));

                if (responce.IsSuccess)
                {
                    var message = new
                    {
                        session

                    };

                    return Ok(message);
                }
                else return BadRequest(responce);
            }
            catch (Exception e)
            {
                Log.Error(e, "handshake error");
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
                    var id = BackgroundJob.Enqueue(() => SaveManifestJob(new SaveManifestCommand(manifestDto.Manifest)));

                    return Ok(new
                    {
                        id
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

        [Queue("manifest")]
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task SaveManifestJob(SaveManifestCommand saveCommandManifest)
        {
            await _mediator.Send(saveCommandManifest);

        }

    }
}
