﻿using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Ct
{
    public interface IAllergiesChronicIllness : IExtract
    {
        Guid Id { get; set; }
        int VisitID { get; set; }
        DateTime VisitDate { get; set; }
        string? FacilityName { get; set; } 
        string? ChronicIllness { get; set; }
        DateTime? ChronicOnsetDate { get; set; }
        string? knownAllergies { get; set; }
        string? AllergyCausativeAgent { get; set; }
        string? AllergicReaction { get; set; }
        string? AllergySeverity { get; set; }
        DateTime? AllergyOnsetDate { get; set; }
        string? Skin { get; set; }
        string? Eyes { get; set; }
        string? ENT { get; set; }
        string? Chest { get; set; }
        string? CVS { get; set; }
        string? Abdomen { get; set; }
        string? CNS { get; set; }
        string? Genitourinary { get; set; }
        

    }
}
