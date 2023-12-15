using DwapiCentral.Mnch.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwapiCentral.Mnch.Domain.Repository
{
    public interface IDocketRepository
    {
        Task<Docket> GetDocketId(string docket);
    }
}
