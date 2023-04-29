using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Shared.Application.Interfaces.Ct
{
    public interface ILaboratory
    {
        int? VisitId { get; set; }
        DateTime? OrderedByDate { get; set; }
        DateTime? ReportedByDate { get; set; }
        string TestName { get; set; }
        int? EnrollmentTest { get; set; }
        string TestResult { get; set; }

        DateTime? DateSampleTaken { get; set; }
        string SampleType { get; set; }
        DateTime? Date_Created { get; set; }
        DateTime? Date_Last_Modified { get; set; }
    }
}
