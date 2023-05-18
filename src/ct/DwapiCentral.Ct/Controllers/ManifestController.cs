using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace DwapiCentral.Ct.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class ManifestController : Controller
    {
        private readonly IMediator _mediator;

        public ManifestController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<HttpResponseMessage> SaveManifest([FromBody] Manifest manifest)
        {
           
                if(manifest !=null)
                {
                    //validate site
                    await _mediator.Send(new ValidateSiteCommand(manifest.SiteCode,manifest.Name));

                   
                    await _mediator.Send(new SaveManifestCommand(manifest));

                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("Manifest saved successfully.")
                    };

                    return response;

                }
            return new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent($"The expected {new Manifest().GetType().Name} is null")
            };
        }
    }
}
