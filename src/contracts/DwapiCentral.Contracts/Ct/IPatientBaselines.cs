using DwapiCentral.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Contracts.Ct
{
     public interface IPatientBaselines : IEntity
    {
          int? bCD4 { get; set; }
          DateTime? bCD4Date { get; set; }
          int? bWAB { get; set; }
          DateTime? bWABDate { get; set; }
          int? bWHO { get; set; }
          DateTime? bWHODate { get; set; }
          int? eWAB { get; set; }
          DateTime? eWABDate { get; set; }
          int? eCD4 { get; set; }
          DateTime? eCD4Date { get; set; }
          int? eWHO { get; set; }
          DateTime? eWHODate { get; set; }
          int? lastWHO { get; set; }
          DateTime? lastWHODate { get; set; }
          int? lastCD4 { get; set; }
          DateTime? lastCD4Date { get; set; }
          int? lastWAB { get; set; }
          DateTime? lastWABDate { get; set; }
          int? m12CD4 { get; set; }
          DateTime? m12CD4Date { get; set; }
          int? m6CD4 { get; set; }
          DateTime? m6CD4Date { get; set; }
          Guid PatientId { get; set; }
    }
}
