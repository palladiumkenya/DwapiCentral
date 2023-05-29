using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> PostManifest([FromBody] Manifest manifest)
        {
            

            if (manifest !=null)
                {
                try
                {
                    //validate site
                    var validFacility = await _mediator.Send(new ValidateSiteCommand(manifest.SiteCode, manifest.Name));

                    if (validFacility.IsSuccess)
                    {
                        var responce = await _mediator.Send(new SaveManifestCommand(manifest));

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
                    else return BadRequest(validFacility.Error.ToString());
                }

                    catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                }
            return BadRequest($"The expected {new Manifest().GetType().Name} is null");
           
        }

    }
}
