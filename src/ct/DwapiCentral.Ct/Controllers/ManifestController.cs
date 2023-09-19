using CSharpFunctionalExtensions;
using DwapiCentral.Contracts.Manifest;
using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Application.Commands.DifferentialCommands;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Application.Interfaces;
using DwapiCentral.Ct.Domain.Models;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;

namespace DwapiCentral.Ct.Controllers
{
    public class ManifestController : Controller
    {
        private readonly IMediator _mediator;
       

        public ManifestController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("api/v3/Spot")]
        public async Task<IActionResult> Post([FromBody] FacilityManifest manifest)
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

                        var jobId = BatchJob.StartNew(x => 
                        { 
                            x.Enqueue(() => Send($"{manifest.Info("Save")}", new SaveManifestCommand(manifest))); 
                        },$"{manifest.Info("Save")}");


                        var masterFacility = ManifestResponse.Create(manifest);
                        masterFacility.JobId = jobId;

                        return Ok(masterFacility);

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
        [Route("api/Spot")]
        public async Task<IActionResult> PostManifest([FromBody] FacilityManifest manifest)
        {
         
            if(null != manifest)
            {

                //validate manifest
                if (!manifest.IsValid())
                {
                    return BadRequest($"Invalid Manifest,Please ensure the SiteCode [{manifest.SiteCode}] is valid and there exists at least one (1) Patient record");
                }
                //validate site
                Log.Debug("checking SiteCode...");
                var validFacility = await _mediator.Send(new ValidateSiteCommand(manifest.SiteCode, manifest.Name));

                if (validFacility.IsSuccess)
                {
                    try
                    {
                        string json = manifest.FacMetrics[0].Metric;

                        dynamic data = JsonConvert.DeserializeObject(json);

                        manifest.EmrVersion = data.EmrVersion;

                        dynamic dwapiVersiondata = JsonConvert.DeserializeObject(manifest.FacMetrics[1].Metric);

                        manifest.DwapiVersion = dwapiVersiondata.Version;

                        var jobId = BatchJob.StartNew(x =>
                        {
                            x.Enqueue(() => Send($"{manifest.Info("Save")}", new MergeDifferentialManifestCommand(manifest)));
                        }, $"{manifest.Info("Save")}");


                        var masterFacility = ManifestResponse.Create(manifest);
                       

                        return Ok(masterFacility);
                    }
                    catch (Exception e)
                    {
                        Log.Error("Clear Site Manifest Error", e);
                    }
                }
                else return BadRequest(validFacility.Error.ToString());

            }
            return BadRequest($"The expected {new Manifest().GetType().Name} is null");

        }

       
        [Queue("manifest")]        
        [AutomaticRetry(Attempts = 3)]
        [DisplayName("{0}")]
        public async Task Send(string jobName, IRequest<Result> command)
        {
            await _mediator.Send(command);
        }

    }
}
