
using DwapiCentral.Contracts.Prep;
using DwapiCentral.Prep.Domain.Models;
using System.Collections.Generic;


namespace DwapiCentral.Prep.Application.DTOs
{
    public class PrepExtractsDto
    { 
        public List<PatientPrepExtract> PatientPrepExtracts { get; set; } = new List<PatientPrepExtract>();
        public List<PrepAdverseEvent> PrepAdverseEventExtracts { get; set; } = new List<PrepAdverseEvent>();
        public List<PrepBehaviourRisk> PrepBehaviourRiskExtracts { get; set; } = new List<PrepBehaviourRisk>();
        public List<PrepCareTermination> PrepCareTerminationExtracts { get; set; } = new List<PrepCareTermination>();

        public List<PrepLab> PrepLabExtracts { get; set; } = new List<PrepLab>();
        public List<PrepPharmacy> PrepPharmacyExtracts { get; set; } = new List<PrepPharmacy>();
        public List<PrepVisit> PrepVisitExtracts { get; set; } = new List<PrepVisit>();
    }
}
