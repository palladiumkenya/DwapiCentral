using System;

namespace DwapiCentral.Shared.Application.Interfaces.Ct
{
    public interface IEntity
    {
        Guid Id { get; set; }
        string Emr { get; set; }
        string Project { get; set; }
        bool Voided { get; set; }
        bool Processed { get; set; }
    }
}