using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Domain.Models;
using Infrastracture.Custom;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.Linq.Expressions;
using System.Net;

namespace DwapiCentral.Ct.Controllers
{
    
    [ApiController]
    public class ManifestController : Controller
    {
        private readonly IMediator _mediator;
       

        public ManifestController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("api/v3/Spot")]
        public async Task<IActionResult> PostManifest([FromBody] FacilityManifest manifest)
        {
            

            if (manifest !=null)
                {
                try
                {
                    //validate site
                    var validFacility = await _mediator.Send(new ValidateSiteCommand(manifest.SiteCode, manifest.Name));

                    if (validFacility.IsSuccess)
                    {

                        string json = manifest.FacMetrics[0].Metric;

                        dynamic data = JsonConvert.DeserializeObject(json);

                        manifest.EmrVersion = data.EmrVersion;

                        dynamic dwapiVersiondata = JsonConvert.DeserializeObject(manifest.FacMetrics[1].Metric);

                        manifest.DwapiVersion = dwapiVersiondata.Version;                     

                                              

                        var responce = await _mediator.Send(new SaveManifestCommand(manifest));
                      if (responce.IsSuccess)
                        {
                            var successMessage = new
                            {
                                manifestDetails = Manifest.Create(manifest)
                                
                            };

                            return Ok(successMessage);

                            
                        }
                        else return BadRequest(responce);

                        
                    }
                    else return BadRequest(validFacility.Error.ToString());
                }

                    catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                }
            return BadRequest($"The expected {new Manifest().GetType().Name} is null");
           
        }

        [HttpPost]
        [Route("api/Handshake/{session}")]        
        public async Task<IActionResult> Post(Guid session)
        {
            try
            {
                var responce = await _mediator.Send(new UpdateHandshakeCommand(session));

                if (responce.IsSuccess)
                {
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Manifest saved successfully.")
                    };


                    return Ok(response);
                }
                else return BadRequest(responce);
            }
            catch (Exception e)
            {
                Log.Error("Handshake error", e);               
                return BadRequest(e.Message);
            }
        }

    }
}
