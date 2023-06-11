using DwapiCentral.Ct.Domain.Models.Extracts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Infrastructure
{
    public class PatientVisitEqualityComparer : IEqualityComparer<PatientVisitExtract>
    {
        public bool Equals(PatientVisitExtract x, PatientVisitExtract y)
        {
            return x.PatientPk == y.PatientPk &&
                x.SiteCode == y.SiteCode &&
                x.VisitId == y.VisitId &&
                x.VisitDate == y.VisitDate;
        }

        public int GetHashCode([DisallowNull] PatientVisitExtract obj)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + obj.PatientPk.GetHashCode();
                hash = hash * 23 + obj.SiteCode.GetHashCode();
                hash = hash * 23 + obj.VisitId.GetHashCode();
                hash = hash * 23 + obj.VisitDate.GetHashCode();
                return hash;
            }
        }
    }
}
