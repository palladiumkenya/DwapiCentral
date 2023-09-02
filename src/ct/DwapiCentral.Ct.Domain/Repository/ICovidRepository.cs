using DwapiCentral.Ct.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Ct.Domain.Repository
{
    public interface ICovidRepository 
    {
        Task MergeAsync(IEnumerable<CovidExtract> covidExtracts);

    }
}
