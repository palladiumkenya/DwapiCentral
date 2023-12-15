using CSharpFunctionalExtensions;
using DwapiCentral.Hts.Application.Commands;
using DwapiCentral.Hts.Domain.Exceptions;
using DwapiCentral.Hts.Domain.Repository;
using DwapiCentral.Shared.Domain.Enums;
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
        private readonly IManifestRepository _manifestRepository;
        private readonly IFacilityRepository _facilityRepository;

        public HtsController(IMediator mediator, IManifestRepository manifestRepository, IFacilityRepository facilityRepository)
        {
            _mediator = mediator;
            _manifestRepository = manifestRepository;
            _facilityRepository = facilityRepository;
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

        [HttpPost]
        [Route("api/Hts/Handshake")]
        public async Task<IActionResult> Post(Guid session)
        {
            try
            {
                var responce = await _mediator.Send(new UpdateHandshakeCommand(session));

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
                Log.Error("Handshake error", e);
                return BadRequest(e.Message);
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
                if (manifest.manifest.Cargoes.Count > 1)
                {
                    string json = manifest.manifest.Cargoes[1].Items;

                    dynamic data = JsonConvert.DeserializeObject(json);

                    manifest.manifest.EmrVersion = data.EmrVersion;

                    dynamic dwapiVersiondata = JsonConvert.DeserializeObject(manifest.manifest.Cargoes[2].Items);

                    manifest.manifest.DwapiVersion = dwapiVersiondata.Version;
                }
                try
                {
                    //var facility = await _facilityRepository.GetByCode(manifest.manifest.SiteCode);
                    //if (null == facility)
                    //    throw new SiteNotEnrolledException(manifest.manifest.SiteCode);

                    //try
                    //{
                    //    if (manifest.manifest.EmrSetup != EmrSetup.Community)
                    //      await  _manifestRepository.ClearFacility(manifest.manifest.SiteCode);
                    //}
                    //catch (Exception e)
                    //{
                    //    Log.Error("Clear MANIFEST ERROR ", e);
                    //}

                    //try
                    //{
                    //    if (manifest.manifest.EmrSetup == EmrSetup.Community)
                    //        _manifestRepository.ClearFacility(manifest.manifest.SiteCode, "IRDO");
                    //}
                    //catch (Exception e)
                    //{
                    //    Log.Error("Clear COMMUNITY MANIFEST ERROR ", e);
                    //}


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
