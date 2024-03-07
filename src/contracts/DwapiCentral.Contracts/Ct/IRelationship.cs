using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Ct
{
    public interface IRelationship : IExtract
    {

        Guid Id { get; set; }
        ulong Mhash { get; set; }
        string? FacilityName { get; set; }
        string? RelationshipToPatient { get; set; }
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }

    }
}
