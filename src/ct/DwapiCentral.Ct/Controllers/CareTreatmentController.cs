using AutoMapper;
using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models.Extracts;
using Infrastracture.Custom;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Ct.Controllers
{
    [ApiController]
    public class CareTreatmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CareTreatmentController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
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


                    var response = await _mediator.Send(new SavePatientCommand(patientExtracts));
                    if (response.IsSuccess)
                    {


                        var successMessage = new
                        {

                            BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                        };
                        return Ok(successMessage);
                    }
                    else
                    {
                        return BadRequest(response.Error);
                    }


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

        [HttpPost]
        [Route("api/v3/Ipt")]
        public async Task<IActionResult> PostBatchNew([FromBody] PatientIptSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    List<PatientIptSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<IptExtract> patientIptExtracts = _mapper.Map<IEnumerable<IptExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeIptCommand(patientIptExtracts));
                    if (response.IsSuccess)
                    {


                        var successMessage = new
                        {

                            BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                        };
                        return Ok(successMessage);
                    }
                    else
                    {
                        return BadRequest(response.Error);
                    }


                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(PatientIptSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new PatientIptSourceBag().GetType().Name}' is null");

        }

        [HttpPost]
        [Route("api/v3/PatientVisits")]
        public async Task<IActionResult> PostBatchNew([FromBody] PatientVisitSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    List<PatientVisitSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<PatientVisitExtract> patientVisitExtracts = _mapper.Map<IEnumerable<PatientVisitExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergePatientVisitCommand(patientVisitExtracts));
                    if (response.IsSuccess)
                    {


                        var successMessage = new
                        {

                            BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                        };
                        return Ok(successMessage);
                    }
                    else
                    {
                        return BadRequest(response.Error);
                    }


                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(PatientVisitSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new PatientVisitSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/PatientArt")]
        public async Task<IActionResult> PostBatchArt([FromBody] PatientArtSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    List<PatientArtSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<PatientArtExtract> patientArtExtracts = _mapper.Map<IEnumerable<PatientArtExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergePatientArtCommand(patientArtExtracts));
                    if (response.IsSuccess)
                    {


                        var successMessage = new
                        {

                            BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                        };
                        return Ok(successMessage);
                    }
                    else
                    {
                        return BadRequest(response.Error);
                    }


                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(PatientArtSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new PatientArtSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/PatientBaselines")]
        public async Task<IActionResult> PostBatchBaseline([FromBody] PatientBaselineSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    List<PatientBaselineSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<PatientBaselinesExtract> patientBaselinesExtracts = _mapper.Map<IEnumerable<PatientBaselinesExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergePatientBaselinesCommand(patientBaselinesExtracts));
                    if (response.IsSuccess)
                    {


                        var successMessage = new
                        {

                            BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                        };
                        return Ok(successMessage);
                    }
                    else
                    {
                        return BadRequest(response.Error);
                    }


                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(PatientBaselineSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new PatientBaselineSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/AllergiesChronicIllness")]
        public async Task<IActionResult> PostBatchNew([FromBody] AllergiesChronicIllnessSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    List<AllergiesChronicIllnessSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<AllergiesChronicIllnessExtract> allergiesChronicIllnessExtract = _mapper.Map<IEnumerable<AllergiesChronicIllnessExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeAllergiesChronicIllnessCommand(allergiesChronicIllnessExtract));
                    if (response.IsSuccess)
                    {


                        var successMessage = new
                        {

                            BatchKey = new List<Guid>() { LiveGuid.NewGuid() }
                        };
                        return Ok(successMessage);
                    }
                    else
                    {
                        return BadRequest(response.Error);
                    }


                }
                catch (Exception ex)
                {
                    Log.Error(new string('*', 30));
                    Log.Error(nameof(AllergiesChronicIllnessSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new AllergiesChronicIllnessSourceBag().GetType().Name}' is null");
        }
    }
}
