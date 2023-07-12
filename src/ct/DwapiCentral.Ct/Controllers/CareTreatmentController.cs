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
        public async Task<IActionResult> PostBatchPatientExtract([FromBody] PatientSourceBag sourceBag)
        {
            if (null != sourceBag && sourceBag.Extracts.Any())
            {
                if (sourceBag.Extracts.Any(x => !x.IsValid()))
                {
                    return BadRequest("Invalid data, please ensure it has Patient, Facility, and at least one (1) Extract");

                }

                try
                {
                    var response = await _mediator.Send(new SavePatientCommand(sourceBag));
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
                    List<ContactListingSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<ContactListingExtract> contactListingExtract = _mapper.Map<IEnumerable<ContactListingExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeContactListingCommand(contactListingExtract));
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
                    List<CovidSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<CovidExtract> covidExtract = _mapper.Map<IEnumerable<CovidExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeCovidExtractsCommand(covidExtract));
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
                    List<DefaulterTracingSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<DefaulterTracingExtract> defaulterTracingExtract = _mapper.Map<IEnumerable<DefaulterTracingExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeDefaulterTracingCommand(defaulterTracingExtract));
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
                    List<DepressionScreeningSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<DepressionScreeningExtract> depressionScreeningExtract = _mapper.Map<IEnumerable<DepressionScreeningExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeDepressionScreeningCommand(depressionScreeningExtract));
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
                    List<DrugAlcoholScreeningSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<DrugAlcoholScreeningExtract> drugAlcoholScreeningExtract = _mapper.Map<IEnumerable<DrugAlcoholScreeningExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeDrugAlcoholScreeningCommand(drugAlcoholScreeningExtract));
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
                    List<EnhancedAdherenceCounselingSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<EnhancedAdherenceCounsellingExtract> enhancedAdheranceExtract = _mapper.Map<IEnumerable<EnhancedAdherenceCounsellingExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeEnhancedAdheranceCommand(enhancedAdheranceExtract));
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
                    List<GbvScreeningSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<GbvScreeningExtract> gbvScreeningExtract = _mapper.Map<IEnumerable<GbvScreeningExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeGbvScreeningCommand(gbvScreeningExtract));
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
                    List<OtzSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<OtzExtract> otzExtract = _mapper.Map<IEnumerable<OtzExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeOtzCommand(otzExtract));
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
                    List<OvcSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<OvcExtract> ovcExtract = _mapper.Map<IEnumerable<OvcExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergeOvcCommand(ovcExtract));
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
                    List<AdverseEventSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<PatientAdverseEventExtract> patientAdverseEventExtract = _mapper.Map<IEnumerable<PatientAdverseEventExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergePatientAdverseCommand(patientAdverseEventExtract));
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
                    List<PharmacySourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<PatientPharmacyExtract> patientPharmacyExtract = _mapper.Map<IEnumerable<PatientPharmacyExtract>>(sourceDtos);


                    var response = await _mediator.Send(new AddOrUpdatePatientPharmacyCommand(patientPharmacyExtract));
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
                    List<StatusSourceDto> sourceDtos = sourceBag.Extracts;
                    IEnumerable<PatientStatusExtract> patientStatusExtract = _mapper.Map<IEnumerable<PatientStatusExtract>>(sourceDtos);


                    var response = await _mediator.Send(new MergePatientStatusCommand(patientStatusExtract));
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
    }
}
