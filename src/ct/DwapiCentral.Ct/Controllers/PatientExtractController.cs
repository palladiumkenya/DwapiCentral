using AutoMapper;
using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Domain.Models.Extracts;
using Infrastracture.Custom;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace DwapiCentral.Ct.Controllers
{
    
    [ApiController]
    public class PatientExtractController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PatientExtractController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("api/v3/Patient")]
        public async Task<IActionResult> PostBatch([FromBody] PatientSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");
                    
                }

                try
                {
                    List<PatientSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<PatientExtract> patientExtracts = _mapper.Map<IEnumerable<PatientExtract>>(sourceDtos);

                    
                    if (sourceBag.HasJobId)
                    {
                          await _mediator.Send(new SavePatientCommand(patientExtracts));
                    }
                    else
                    {
                           await _mediator.Send(new SavePatientCommand(patientExtracts));
                    }

                    var response = new
                    {
                        
                        BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                    };
                    return Ok(response);

                  
                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(PatientSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new PatientSourceBag().GetType().Name}' is null");
        }
    }
}
