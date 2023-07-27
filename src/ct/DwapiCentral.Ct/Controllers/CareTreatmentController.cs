using AutoMapper;
using DwapiCentral.Ct.Application.Commands;
using DwapiCentral.Ct.Application.DTOs.Source;
using DwapiCentral.Ct.Application.DTOs;
using DwapiCentral.Ct.Domain.Models.Extracts;
using Infrastracture.Custom;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Hangfire;
using System.ComponentModel;
using CSharpFunctionalExtensions;
using System.Net;

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
        [Route("api/v3/Ipt")]
        public async Task<IActionResult> PostBatchIptExtract([FromBody] PatientIptSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    

                    var response = await _mediator.Send(new MergeIptCommand(sourceBag));
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
        public async Task<IActionResult> PostBatchPatientVisits([FromBody] PatientVisitSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    
                    var response = await _mediator.Send(new MergePatientVisitCommand(sourceBag));
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
                                       
                    var response = await _mediator.Send(new MergePatientArtCommand(sourceBag));
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
                    
                    var response = await _mediator.Send(new MergePatientBaselinesCommand(sourceBag));
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
        public async Task<IActionResult> PostBatchAllergies([FromBody] AllergiesChronicIllnessSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeAllergiesChronicIllnessCommand(sourceBag));
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

        [HttpPost]
        [Route("api/v3/ContactListing")]
        public async Task<IActionResult> PostBatchContactListing([FromBody]ContactListingSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeContactListingCommand(sourceBag));
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
                    Log.Error(nameof(ContactListingSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new ContactListingSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/Covid")]
        public async Task<IActionResult> PostBatchCovidExtract([FromBody]CovidSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeCovidExtractsCommand(sourceBag));
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
                    Log.Error(nameof(CovidSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new CovidSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/DefaulterTracing")]
        public async Task<IActionResult> PostBatchDefaulterTracing([FromBody] DefaulterTracingSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeDefaulterTracingCommand(sourceBag));
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
                    Log.Error(nameof(DefaulterTracingSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new DefaulterTracingSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/DepressionScreening")]
        public async Task<IActionResult> PostBatchDepressionScreening([FromBody] DepressionScreeningSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeDepressionScreeningCommand(sourceBag));
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
                    Log.Error(nameof(DepressionScreeningSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new DepressionScreeningSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/DrugAlcoholScreening")]
        public async Task<IActionResult> PostBatchDrugAlcoholScreening([FromBody] DrugAlcoholScreeningSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeDrugAlcoholScreeningCommand(sourceBag));
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
                    Log.Error(nameof(DrugAlcoholScreeningSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new DrugAlcoholScreeningSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/EnhancedAdherenceCounselling")]
        public async Task<IActionResult> PostBatchEnhancedAdherance([FromBody] EnhancedAdherenceCounsellingSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeEnhancedAdheranceCommand(sourceBag));
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
                    Log.Error(nameof(EnhancedAdherenceCounsellingSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new EnhancedAdherenceCounsellingSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/GbvScreening")]
        public async Task<IActionResult> PostBatchGbvScreening([FromBody] GbvScreeningSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeGbvScreeningCommand(sourceBag));
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
                    Log.Error(nameof(GbvScreeningSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new GbvScreeningSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/Otz")]
        public async Task<IActionResult> PostBatchOtz([FromBody] OtzSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeOtzCommand(sourceBag));
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
                    Log.Error(nameof(OtzSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new OtzSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/Ovc")]
        public async Task<IActionResult> PostBatchOvc([FromBody] OvcSourceBag sourceBag)
        {

            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeOvcCommand(sourceBag));
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
                    Log.Error(nameof(OvcSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new OvcSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/PatientAdverseEvents")]
        public async Task<IActionResult> PostBatchAdverseEvent([FromBody] AdverseEventSourceBag sourceBag)
        {

            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergePatientAdverseCommand(sourceBag));
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
                    Log.Error(nameof(AdverseEventSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new AdverseEventSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/PatientLabs")]
        public async Task<IActionResult> PostBatchLabExtracts([FromBody] LaboratorySourceBag sourceBag)
        {

            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    


                    var response = await _mediator.Send(new MergePatientLabsCommand(sourceBag));
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
                    Log.Error(nameof(LaboratorySourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new LaboratorySourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/PatientPharmacy")]
        public async Task<IActionResult> PostBatchPharmacyExtracts([FromBody] PharmacySourceBag sourceBag)
        {

            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new AddOrUpdatePatientPharmacyCommand(sourceBag));
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
                    Log.Error(nameof(PharmacySourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new PharmacySourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/PatientStatus")]
        public async Task<IActionResult> PostBatchStatusExtract([FromBody] StatusSourceBag sourceBag)
        {

            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergePatientStatusCommand(sourceBag));
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
                    Log.Error(nameof(StatusSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new StatusSourceBag().GetType().Name}' is null");
        }

        [HttpPost]
        [Route("api/v3/CervicalCancerScreening")]
        public async Task<IActionResult> PostBatchCervicalCancerScreening([FromBody] CervicalCancerScreeningSourceBag sourceBag)
        {

            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new MergeCervicalCancerScreeningCommand(sourceBag));
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
                    Log.Error(nameof(CervicalCancerScreeningSourceBag), ex);
                    Log.Error(new string('*', 30));
                    return BadRequest(ex);
                }
            }

            return BadRequest($"The expected '{new CervicalCancerScreeningSourceBag().GetType().Name}' is null");
        }

        

    }
}
