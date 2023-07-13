using DwapiCentral.Shared.Application.Interfaces.Ct;
using DwapiCentral.Shared.Domain.Enums;
using System;


namespace DwapiCentral.Ct.Domain.Models.Stage
{
    public abstract class StageExtract : IStage
    {
        public Guid Id { get; set; }
        
        public Guid? FacilityId { get; set; }
        public Guid? CurrentPatientId { get; set; }
        public Guid? LiveSession { get; set; }
        public LiveStage LiveStage { get; set; }
        public DateTime? Generated { get; set; } = DateTime.Now;        
        public string? Emr { get; set; }
        public string? Project { get; set; }       
        public bool Processed { get; set; }
    }
}
